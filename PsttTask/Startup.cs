using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PsttTask.Domain.Entities;
using PsttTask.Infrastucture;
using Scrutor;
using System.Text;

namespace PsttTask.Company
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers();


            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<PsttTaskContext>(options =>
            options.UseSqlServer(
            Configuration?.GetConnectionString("PsttTaskDbConnectionString"))
            .EnableSensitiveDataLogging());


            services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders()
              .AddEntityFrameworkStores<PsttTaskContext>();

            ConfigureDI(services);
            JWTConfigure(services);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(PsttTask.ApplicationService.AssemblyReference.Assembly));
            services.AddAutoMapper(PsttTask.ApplicationService.AssemblyReference.Assembly);
            services.AddValidatorsFromAssembly(PsttTask.ApplicationService.AssemblyReference.Assembly);
            services.AddFluentValidationAutoValidation();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected virtual void ConfigureCors(CorsPolicyBuilder corsPolicyBuilder)
        {
            if (!string.IsNullOrWhiteSpace(Configuration["PsttTaskSettings:CorsOrigins"]))
            {
                corsPolicyBuilder.WithOrigins(Configuration["PsttTaskSettings:CorsOrigins"]!.Split(',')).AllowAnyHeader().AllowAnyMethod()
                    .AllowCredentials();
            }
        }

        protected virtual void ConfigureDI(IServiceCollection services)
        {

            services.Scan(selector => selector
           .FromAssemblies(
               typeof(PsttTask.Domain.AssemblyReference).Assembly,
               typeof(AssemblyReference).Assembly)
           .AddClasses(publicOnly: false)
           .UsingRegistrationStrategy(RegistrationStrategy.Skip)
           .AsMatchingInterface()
           .WithScopedLifetime());
        }

        private void JWTConfigure(IServiceCollection services)
        {
            services.AddAuthentication(cgf =>
            {
                cgf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cgf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });
        }


    }
}
