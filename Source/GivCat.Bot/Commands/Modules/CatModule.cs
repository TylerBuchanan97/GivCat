namespace GivCat.Bot.Commands.Modules
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Discord.Commands;

    using Newtonsoft.Json.Linq;

    [Group("cat")]
    public class CatModule : ModuleBase
    {
        [Command, Summary("Posts a cat picture!")]
        public async Task Cat()
        {
            HttpResponseMessage response =
                await new HttpClient().GetAsync("https://api.thecatapi.com/v1/images/search");

            JArray responseContent = JArray.Parse(await response.Content.ReadAsStringAsync());

            string imageUrl = responseContent?.First["url"]?.Value<string>();

            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                await ReplyAsync(imageUrl);
            }
        }
    }
}