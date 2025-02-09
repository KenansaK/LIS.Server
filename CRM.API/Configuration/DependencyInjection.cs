using FluentValidation;
using CRM.Application.Validators;
using CRM.Application.Commands;
using Kernal.MessageBroker;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System.Reflection;
using Kernal.Interfaces;
using CRM.Application.Behaviors;
using CRM.Infrastructure;
using Kernal.Chaching;
using CRM.Infrastructure;
using Microsoft.AspNetCore.Identity;
using CRM.Application.Requests;
using Kernal.Jwt;
using CRM.Infrastructure.Messaging;
using Kernal.Helpers;
using Kernal.Contracts;
using Persistence.Implementation;
using Kernel.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.API.Configuration
{
    public static class DependencyInjection
    {

        public static IServiceCollection RegisterPersistencfe(this IServiceCollection services)
        {


            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }


        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CRMDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<DbContext>(provider => provider.GetRequiredService<CRMDbContext>());

            services.AddSingleton(sp =>
            {
                return new ConnectionFactory
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest"
                };
            });
            services.AddHttpClient("");
            return services;
        }

        public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "RedisDemo_";
            });

            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }

        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));
            var jwtSettings = configuration.GetSection("Jwt");
            var secretKey = jwtSettings["Secret"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey))
                };
            });

            services.AddAuthorization();
            services.AddScoped<IJwtService, JwtService>();
            services.AddSingleton<IHttpContext, SharedKernel.HttpContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services
            //    .AddFluentEmail(configuration["Email:SenderEmail"], configuration["Email:Sender"])
            //    .AddSmtpSender(configuration["Email:Host"], configuration.GetValue<int>("Email:Port"));

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    Assembly.GetExecutingAssembly(),
                    typeof(AddCustomerCommand).Assembly
                );
            });

            services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateCustomerRequest>();
            //services.AddValidatorsFromAssemblyContaining<LoginDTOValidator>();


            // Register the validation pipeline behavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
            services.AddTransient<Dispatcher>();

            services.Scan(scan =>
                scan.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
        .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
        .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime());
            //services.AddScoped<IRoleService>();
            return services;

        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Library API",
                    Version = "V1",
                    Description = "Library API for Book Management"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(IRabbitMQSender<>), typeof(RabbitMQSender<>));
            services.AddSingleton(typeof(IRabbitMQReceiver<>), typeof(RabbitMQReceiver<>));

            services.Configure<ServiceBusSettings>(configuration.GetSection("ServiceBusSettings"));

            // Register the Publisher and Subscriber as Singletons
            services.AddSingleton<IMessagePublisher, AzureServiceBusPublisher>();
            services.AddSingleton<IMessageSubscriber, AzureServiceBusSubscriber>();

            return services;
        }

        public static IServiceCollection AddMediatr(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    Assembly.GetExecutingAssembly(),
                    typeof(AddCustomerCommand).Assembly // Add other relevant assemblies here
                );
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    Assembly.GetExecutingAssembly(),
                    typeof(CreateCustomerRequest).Assembly // Add other relevant assemblies here
                );
            });
            // services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<LoginRequest>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddCustomerCommand>());
            return services;
        }

        public static IServiceCollection Add(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }


    }
}
