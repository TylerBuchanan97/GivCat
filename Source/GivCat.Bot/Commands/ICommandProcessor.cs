namespace GivCat.Bot.Commands
{
    using System.Threading.Tasks;

    using Discord;
    using Discord.WebSocket;

    public interface ICommandProcessor
    {
        Task ProcessCommand(SocketMessage socketMessage);
    }
}