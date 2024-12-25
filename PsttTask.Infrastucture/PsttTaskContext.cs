using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PsttTask.Domain.Contracts;
using PsttTask.Domain.Entities;
using PsttTask.Infrastructure.Data;

namespace PsttTask.Infrastucture
{
    public class PsttTaskContext : IdentityDbContext<IdentityUser>, IDisposable
    {

        private readonly UserIdentity userIdentity;
        public PsttTaskContext()
        {
            userIdentity = new UserIdentity();
        }

        public PsttTaskContext(DbContextOptions<PsttTaskContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Company>().HasQueryFilter(company => !company.IsDeleted);
            builder.Entity<Branch>().HasQueryFilter(branch => !branch.IsDeleted);


            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                Console.WriteLine($"Environment = {environment}");

                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", false, true)
                   .AddJsonFile($"appsettings.{environment}.json", false, true)
                   .Build();

                var connectionString = configuration.GetConnectionString("PsttTaskDbConnectionString");
                Console.WriteLine($"ConnectionString = {connectionString}");
                optionsBuilder.UseSqlServer(connectionString);
            }

            var loggerFactory = LoggerFactory.Create(builder =>
            {

            });

            optionsBuilder
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAggregateRoot
                            && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var identityEmail = userIdentity?.Email ?? "Unknown";
            var dateNow = DateTime.Now;
            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is IEntity entity)
                {
                    if (entry.State == EntityState.Added)
                        entity.Reference = Guid.NewGuid();
                }
                if (entry.Entity is IAggregateRoot AggregateRoot)
                {
                    if (entry.State == EntityState.Added)
                    {
                        AggregateRoot.CreatedBy = identityEmail;
                        AggregateRoot.CreationDate = dateNow;
                    }
                    else
                    {
                        Entry(AggregateRoot).Property(x => x.CreatedBy).IsModified = false;
                        Entry(AggregateRoot).Property(x => x.CreationDate).IsModified = false;
                        AggregateRoot.UpdatedBy = identityEmail;
                        AggregateRoot.UpdationDate = dateNow;
                    }
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAggregateRoot
                            && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var identityMail = userIdentity?.Email ?? "Unknown";
            var dateNow = DateTime.Now;
            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is IAggregateRoot entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = entity.UpdatedBy = identityMail;
                        entity.CreationDate = dateNow;
                    }
                    else
                    {
                        Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        Entry(entity).Property(x => x.CreationDate).IsModified = false;
                        entity.UpdatedBy = identityMail;
                        entity.UpdationDate = dateNow;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
