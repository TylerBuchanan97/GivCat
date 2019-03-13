namespace GivCat.Bot.Commands.Modules
{
    using System.Threading.Tasks;

    using Discord.Commands;

    [Group("cat")]
    public class CatModule : ModuleBase
    {
        [Command, Summary("Posts a cat picture!")]
        public async Task Default()
        {
            await ReplyAsync("Test Message");
        }
    }
}