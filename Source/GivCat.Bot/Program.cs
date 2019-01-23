namespace GivCat.Bot
{
    using System;
    using System.Threading.Tasks;

    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;

    using GivCat.Api.Common;
    using GivCat.Api.Models;
    using GivCat.Api.Request;
    using GivCat.Bot.Commands;
    using GivCat.Bot.Initialization;

    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Bot key not provided!");

                Environment.Exit(0);
            }

            IServiceProvider serviceProvider = CreateServiceProvider();

            await serviceProvider.GetService<IBotInitializer>().InitializeGivCatBot(args[0]);

            await Task.Delay(-1);
        }

        private static ServiceProvider CreateServiceProvider()
        {
            return new ServiceCollection().AddTransient<IBotInitializer, BotInitializer>()
                .AddTransient<ICommandProcessor, CommandProcessor>()
                .AddTransient<IRequestSender<CatApiRequest, CatApiResponse>, CatApiRequestSender>()
                .AddSingleton(new CommandService())
                .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig { LogLevel = LogSeverity.Info }))
                .BuildServiceProvider();
        }
    }
}