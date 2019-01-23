namespace GivCat.Bot.Commands.Modules
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Discord;
    using Discord.Commands;

    using GivCat.Api.Common;
    using GivCat.Api.Models;

    using Newtonsoft.Json.Linq;

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

            string mediaUrl = catApiResponse.Url;

            if (!string.IsNullOrWhiteSpace(mediaUrl))
            {
                string breed = string.Empty;

                IList<JObject> breedsList = catApiResponse.Breeds.ToList();

                if (breedsList.Any())
                {
                    breed = $"Breed: {breedsList.FirstOrDefault()?["name"] ?? string.Empty}";
                }

                await ReplyAsync(embed: new EmbedBuilder().WithImageUrl(mediaUrl).WithFooter(breed).Build());
            }
        }
    }
}