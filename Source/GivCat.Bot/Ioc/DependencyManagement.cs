namespace GivCat.Bot.Ioc
{
    using System;

    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;

    using GivCat.Bot.Commands;
    using GivCat.Bot.Initialization;

    using Microsoft.Extensions.DependencyInjection;

    using QuickConfiguration.Extensions;

    public class DependencyManagement
    {
        private static IServiceProvider provider;

        public static IServiceProvider Provider => provider ?? (provider = CreateServiceProvider());

        private static IServiceProvider CreateServiceProvider()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddDefaultConfiguration("ENVIRONMENT", "appsettings");

            serviceCollection.AddTransient<IBotInitializer, BotInitializer>();
            serviceCollection.AddTransient<ICommandProcessor, CommandProcessor>();
            serviceCollection.AddSingleton(new CommandService());
            serviceCollection.AddSingleton(
                new DiscordSocketClient(new DiscordSocketConfig { LogLevel = LogSeverity.Info }));

            return serviceCollection.BuildServiceProvider();
        }
    }
}