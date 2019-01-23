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

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main()
        {
            IConfiguration configuration = CreateConfiguration();

            IServiceProvider serviceProvider = CreateServiceProvider(configuration);

            await serviceProvider.GetService<IBotInitializer>().InitializeGivCatBot();

            await Task.Delay(-1);
        }

        private static IConfiguration CreateConfiguration()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true).AddJsonFile($"appsettings.{environment}.json", true, true)
                .Build();
        }

        private static ServiceProvider CreateServiceProvider(IConfiguration configuration)
        {
            return new ServiceCollection().AddTransient<IBotInitializer, BotInitializer>()
                .AddTransient<ICommandProcessor, CommandProcessor>()
                .AddTransient<IRequestSender<CatApiRequest, CatApiResponse>, CatApiRequestSender>()
                .AddSingleton(configuration).AddSingleton(new CommandService()).AddSingleton(
                    new DiscordSocketClient(new DiscordSocketConfig { LogLevel = LogSeverity.Info }))
                .BuildServiceProvider();
        }
    }
}