namespace GivCat.Api.Request
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using GivCat.Api.Common;
    using GivCat.Api.Models;

    using Newtonsoft.Json.Linq;

    public class CatApiRequestSender : IRequestSender<CatApiRequest, CatApiResponse>
    {
        public async Task<CatApiResponse> SendRequestAsync(CatApiRequest request)
        {
            HttpResponseMessage response = await new HttpClient().GetAsync(request.BaseRequestUrl);

            JArray responseContent = JArray.Parse(await response.Content.ReadAsStringAsync());

            string imageUrl = responseContent?.First["url"]?.Value<string>();

            return !string.IsNullOrWhiteSpace(imageUrl) ? new CatApiResponse { Url = imageUrl } : null;
        }
    }
}