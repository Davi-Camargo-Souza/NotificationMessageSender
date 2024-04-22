using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationMessageSender.API.Services;
using NotificationMessageSender.Core.Common.Interfaces.Data;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
using NotificationMessageSender.Core.Common.Interfaces.Services;
using NotificationMessageSender.Core.MessageBus.Services;
using NotificationMessageSender.Core.MessageBus.Services.Interfaces;
using NotificationMessageSender.Infraestructure.Context;
using NotificationMessageSender.Infraestructure.Repositories;
using NotificationMessageSender.Worker.BackgroundServices;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(NotificationMessageSender.Worker.Application.Commands.Send.Base.SendNotificationCommand).Assembly));
        services.AddScoped<IMessageBus, MessageBus>();

        //repositorios
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        //contexts
        var connectionString = hostContext.Configuration.GetConnectionString("PostgreSQL");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("NotificationMessageSender.API")));
        services.AddTransient(_ => new DapperContext(connectionString));

        services.AddTransient<IEmailSenderService, EmailSenderService>();
        services.AddHttpContextAccessor();


        services.AddHostedService<SendNotificationWorker>();
    })
    .Build();

await host.RunAsync();