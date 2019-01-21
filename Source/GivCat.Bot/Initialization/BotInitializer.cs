namespace GivCat.Bot.Initialization
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;

    using GivCat.Bot.Commands;

    public class BotInitializer : IBotInitializer
    {
        private readonly DiscordSocketClient client;

        private readonly ICommandProcessor commandProcessor;

        private readonly CommandService commandService;

        private readonly IServiceProvider serviceProvider;

        public BotInitializer(
            DiscordSocketClient client,
            ICommandProcessor commandProcessor,
            CommandService commandService,
            IServiceProvider serviceProvider)
        {
            this.client = client;
            this.commandProcessor = commandProcessor;
            this.commandService = commandService;
            this.serviceProvider = serviceProvider;
        }

        public async Task InitializeGivCatBot(string botKey)
        {
            RegisterEventListeners();

            await RegisterModules(serviceProvider);

            await client.LoginAsync(TokenType.Bot, botKey);

            await client.StartAsync();
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