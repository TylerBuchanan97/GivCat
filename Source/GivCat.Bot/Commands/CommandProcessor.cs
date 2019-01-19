namespace GivCat.Bot.Commands
{
    using System.Threading.Tasks;

    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;

    public class CommandProcessor : ICommandProcessor
    {
        private readonly DiscordSocketClient client;

        private readonly CommandService commandService;

        public CommandProcessor(DiscordSocketClient client, CommandService commandService)
        {
            this.client = client;
            this.commandService = commandService;
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

            IResult result = await commandService.ExecuteAsync(context, argPos, null);

            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        private bool MessageIsValid(IUserMessage message, ref int argPos)
        {
            return message.HasStringPrefix("giv ", ref argPos)
                   || message.HasMentionPrefix(client.CurrentUser, ref argPos);
        }
    }
}