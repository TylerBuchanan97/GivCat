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
            return new ServiceCollection().AddTransient<IBotInitializer, BotInitializer>()
                .AddTransient<ICommandProcessor, CommandProcessor>().AddSingleton(CreateConfiguration())
                .AddSingleton(new CommandService())
                .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig { LogLevel = LogSeverity.Info }))
                .BuildServiceProvider();
        }
    }
}