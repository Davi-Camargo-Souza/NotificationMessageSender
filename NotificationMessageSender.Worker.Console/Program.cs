using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationMessageSender.Core.Common.Interfaces.Data;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
using NotificationMessageSender.Core.MessageBus.Services;
using NotificationMessageSender.Core.MessageBus.Services.Interfaces;
using NotificationMessageSender.Infraestructure.Repositories;
using NotificationMessageSender.Worker.BackgroundServices;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });



        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        services.AddScoped<IMessageBus, MessageBus>();

        services.AddHostedService<SendNotificationWorker>();
    })
    .Build();

await host.RunAsync();