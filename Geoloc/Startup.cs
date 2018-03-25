using System;
using AutoMapper;
using Geoloc.Data;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Services;
using Geoloc.Services.Abstract;
using Geoloc.Services.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddScoped<IUserRelationService, UserRelationService>();
            services.AddScoped<IUserRelationRepository, UserRelationRepository>();


            services.AddIdentity<AppUser, UserRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<UserStore<AppUser, UserRole, ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<UserRole, ApplicationDbContext, Guid>>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
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
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
