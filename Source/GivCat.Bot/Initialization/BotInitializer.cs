namespace GivCat.Bot.Initialization
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;

    using GivCat.Bot.Commands;

    using Microsoft.Extensions.Configuration;

    public class BotInitializer : IBotInitializer
    {
        private readonly DiscordSocketClient client;

        private readonly ICommandProcessor commandProcessor;

        private readonly CommandService commandService;

        private readonly IConfiguration configuration;

        private readonly IServiceProvider serviceProvider;

        public BotInitializer(
            DiscordSocketClient client,
            ICommandProcessor commandProcessor,
            CommandService commandService,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            this.client = client;
            this.commandProcessor = commandProcessor;
            this.commandService = commandService;
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
        }

        public async Task InitializeGivCatBot()
        {
            RegisterEventListeners();

            await RegisterModules(serviceProvider);

            string botKey = GetBotKey();

            await client.LoginAsync(TokenType.Bot, botKey);

            await client.StartAsync();
        }

        private string GetBotKey()
        {
            return configuration["Bot Token"];
        }

        private Task ProcessLogMessage(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.ToString());

            return Task.CompletedTask;
        }

        private async Task ProcessReceivedMessage(SocketMessage socketMessage)
        {
            await commandProcessor.ProcessCommand(socketMessage);
        }

        private void RegisterEventListeners()
        {
            client.Log += ProcessLogMessage;

            client.MessageReceived += ProcessReceivedMessage;
        }

        private async Task RegisterModules(IServiceProvider serviceProvider)
        {
            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
        }
    }
}