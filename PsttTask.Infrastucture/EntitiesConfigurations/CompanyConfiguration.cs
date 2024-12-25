using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsttTask.Domain.Entities;

namespace PsttTask.Infrastucture.EntitiesConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(nameof(Company)).HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();

            builder.HasIndex(c => c.Reference).IsUnique();
            builder.Property(c => c.Reference).HasDefaultValueSql("newid()").ValueGeneratedOnAdd();
        }
    }
}
