using AutoMapper;
using Library.DataBase;
using Library.Domain.Entities;
using Library.MapperProfile;
using Library.Services.Commands;
using Library.Services.Quartz;
using Library.Services.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;

namespace Library.Extensions
{
    public static class DependencyInjectionExtenstions
    {
        public static void AddMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BookProfile());
                mc.AddProfile(new GenreProfile());
                mc.AddProfile(new AuthorProfile());
                mc.AddProfile(new PublisherProfile());
                mc.AddProfile(new OrderProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new RoleProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddQueries(this IServiceCollection services)
        {
            services.AddScoped<BookQueries>();
            services.AddScoped<AuthorQueries>();
            services.AddScoped<GenresQueries>();
            services.AddScoped<PublishersQueries>();
            services.AddScoped<BookStatusQueries>();
            services.AddScoped<UserQueries>();
            services.AddScoped<RoleQueries>();
        }

        public static void AddCommands(this IServiceCollection services)
        {
            services.AddScoped<UserCommands>();
            services.AddScoped<AuthorCommands>();
            services.AddScoped<BookCommands>();
            services.AddScoped<GenreCommands>();
            services.AddScoped<PublisherCommands>();
            services.AddScoped<RoleCommands>();
            services.AddScoped<OrderCommands>();
        }

        public static void AddQuartz(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.AddJob<OrdersCheker>(options =>
                {
                    options.WithIdentity("trigger1", "group1")
                        .Build();
                });

                q.AddTrigger(options =>
                {
                    options.ForJob("trigger1", "group1")
                        .StartNow()
                        .WithSimpleSchedule(x =>
                            x.WithIntervalInSeconds(1)
                                .RepeatForever());
                });
            });

            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        }

        public static void AddIdentity(this IServiceCollection services)
        {

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            })
            .AddDefaultTokenProviders()
            .AddDefaultUI()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthorization();
            services.AddAuthentication();
        }
    }
}
