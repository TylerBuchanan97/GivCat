namespace GivCat.Bot.Commands
{
    using System;
    using System.Threading.Tasks;

    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;

    public class CommandProcessor : ICommandProcessor
    {
        private readonly DiscordSocketClient client;

        private readonly CommandService commandService;

        private readonly IServiceProvider serviceProvider;

        public CommandProcessor(
            DiscordSocketClient client,
            CommandService commandService,
            IServiceProvider serviceProvider)
        {
            this.client = client;
            this.commandService = commandService;
            this.serviceProvider = serviceProvider;
        }

        public async Task ProcessCommand(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage message))
            {
                return;
            }

            int argPos = 0;

            if (!MessageIsValid(message, ref argPos))
            {
                return;
            }

            CommandContext context = new CommandContext(client, message);

            await commandService.ExecuteAsync(context, argPos, serviceProvider);
        }

        private bool MessageIsValid(IUserMessage message, ref int argPos)
        {
            return message.HasStringPrefix("giv ", ref argPos)
                   || message.HasMentionPrefix(client.CurrentUser, ref argPos);
        }
    }
}