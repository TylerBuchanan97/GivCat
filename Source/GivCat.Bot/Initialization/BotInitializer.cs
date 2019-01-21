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

        public BotInitializer(
            DiscordSocketClient client,
            ICommandProcessor commandProcessor,
            CommandService commandService)
        {
            this.client = client;
            this.commandProcessor = commandProcessor;
            this.commandService = commandService;
        }

        public async Task InitializeGivCatBot(string botKey, IServiceProvider serviceProvider)
        {
            RegisterEventListeners();

            await RegisterModules(serviceProvider);

            await client.LoginAsync(TokenType.Bot, botKey);

            await client.StartAsync();
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

        private Task ProcessLogMessage(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.ToString());

            return Task.CompletedTask;
        }

        private async Task RegisterModules(IServiceProvider serviceProvider)
        {
            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
        }
    }
}