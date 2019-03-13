namespace GivCat.Bot.Ioc
{
    using System;

    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;

    using GivCat.Bot.Commands;
    using GivCat.Bot.Initialization;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyManagement
    {
        private static IServiceProvider provider;

        public static IServiceProvider Provider => provider ?? (provider = CreateServiceProvider());

        private static IConfiguration CreateConfiguration()
        {
            string environment = Environment.GetEnvironmentVariable("ENVIRONMENT");

            return new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true).AddJsonFile($"appsettings.{environment}.json", true, true)
                .Build();
        }

        private static IServiceProvider CreateServiceProvider()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IBotInitializer, BotInitializer>();
            serviceCollection.AddTransient<ICommandProcessor, CommandProcessor>();
            serviceCollection.AddSingleton(CreateConfiguration());
            serviceCollection.AddSingleton(new CommandService());
            serviceCollection.AddSingleton(
                new DiscordSocketClient(new DiscordSocketConfig { LogLevel = LogSeverity.Info }));

            return serviceCollection.BuildServiceProvider();
        }
    }
}