namespace GivCat.Bot.Commands.Modules
{
    using System.Threading.Tasks;

    using Discord;
    using Discord.Commands;

    using GivCat.Api.Common;
    using GivCat.Api.Models;

    [Group("cat")]
    public class CatModule : ModuleBase
    {
        private readonly IRequestSender<CatApiRequest, CatApiResponse> catApiRequestSender;

        public CatModule(IRequestSender<CatApiRequest, CatApiResponse> catApiRequestSender)
        {
            this.catApiRequestSender = catApiRequestSender;
        }

        [Command, Summary("Posts a cat picture!")]
        public async Task Default()
        {
            CatApiResponse catApiResponse = await catApiRequestSender.SendRequestAsync(new CatApiRequest());

            if (catApiResponse == null)
            {
                return;
            }

            string mediaUrl = catApiResponse.MediaUrl;

            if (!string.IsNullOrWhiteSpace(mediaUrl))
            {
                await ReplyAsync(embed: new EmbedBuilder().WithImageUrl(mediaUrl).Build());
            }
        }
    }
}