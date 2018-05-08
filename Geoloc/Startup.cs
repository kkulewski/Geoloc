using AutoMapper;
using Geoloc.Data;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Services;
using Geoloc.Services.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Geoloc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();
            services.AddCors();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();

            services.AddScoped<IRelationService, RelationService>();
            services.AddScoped<IRelationRepository, RelationRepository>();
            
            services.AddScoped<IMeetingService, MeetingService>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();

            services.AddSingleton<JwtTokenFactory>();
            services.AddScoped<IAuthService, AuthService>();

            var builder = services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(UserRole), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = Configuration.GetSection("JwtTokens")["Issuer"],
                        ValidAudience = Configuration.GetSection("JwtTokens")["Audience"],
                        IssuerSigningKey = JwtTokenFactory.GetSecurityKey(Configuration.GetSection("JwtTokens")["Key"])
                    };
                });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LocalDb"), b => b.MigrationsAssembly("Geoloc"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
