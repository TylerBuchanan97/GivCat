namespace GivCat.Bot.Initialization
{
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

        public async Task InitializeGivCatBot(string botKey)
        {
            RegisterEventListeners();

            await RegisterModules();

            await client.LoginAsync(TokenType.Bot, botKey);

            await client.StartAsync();
        }

        private async Task ProcessReceivedMessage(SocketMessage socketMessage)
        {
            await commandProcessor.ProcessCommand(socketMessage);
        }

        private void RegisterEventListeners()
        {
            client.MessageReceived += ProcessReceivedMessage;
        }

        private async Task RegisterModules()
        {
            await commandService.AddModulesAsync(Assembly.GetExecutingAssembly(), null);
        }
    }
}