﻿using Loquimini.Data;
using Loquimini.Interfaces.Repository;
using Loquimini.Manager;
using Loquimini.Manager.Interfaces;
using Loquimini.Model.Entities;
using Loquimini.Repository;
using Loquimini.Repository.UnitOfWork;
using Loquimini.Service;
using Loquimini.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loquimini.API
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];

                services.AddDbContext<LoquiminiDbContext>(options =>
                { 
                    options.UseSqlServer(connectionString); 
                });

                services.AddScoped<DbContext, LoquiminiDbContext>();

                services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<LoquiminiDbContext>()
                    .AddDefaultTokenProviders();
            
            #region User Managers

            services.AddScoped<IDatabaseManager, DatabaseManager>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IIdentityManager, IdentityManager>();

            #endregion

            #region Services

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReceiptService, ReceiptService>();

            #endregion

            #region Repositories

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(ITrackableRepository<,>), typeof(TrackableRepository<,>));

            #endregion

            #region CurrentPrincipal

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(provider => provider.GetService<IHttpContextAccessor>()?.HttpContext?.User);

            #endregion

            return services;
        }
    }
}