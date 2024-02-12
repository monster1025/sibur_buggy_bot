using Autofac;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using Sibur.BuggyBot.App;
using Sibur.BuggyBot.Messaging;
using Sibur.BuggyBot.Settings;
using Telegram.Bot;

namespace Sibur.BuggyBot.Di;

public class DI
{
    public IContainer Build()
    {
        var container = new ContainerBuilder();

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        container.Register(c => config).As<IConfiguration>().SingleInstance();
        container.RegisterType<Messaging.Telegram>().As<ITelegram>().SingleInstance();

        container.RegisterType<App.App>().As<IApp>();
        container.Register(c =>
            {
                var settings = c.Resolve<IConfiguration>().GetSection("Telegram").Get<TelegramSettings>()!; ;
                Console.WriteLine(JsonConvert.SerializeObject(settings));
                return settings;
            })
            .As<TelegramSettings>().SingleInstance();

        container.Register(c => LogManager.GetLogger("main")).As<ILogger>();
        container.Register(c =>
        {
            var client = new TelegramBotClient(c.Resolve<TelegramSettings>().Token);
            return client;
        }).AsSelf().SingleInstance();

        return container.Build();
    }
}