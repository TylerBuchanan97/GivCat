namespace GivCat.Api.Request
{
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using GivCat.Api.Common;
    using GivCat.Api.Models;

    using Newtonsoft.Json;

    public class CatApiRequestSender : IRequestSender<CatApiRequest, CatApiResponse>
    {
        public async Task<CatApiResponse> SendRequestAsync(CatApiRequest request)
        {
            HttpResponseMessage rawResponse = await new HttpClient().GetAsync(request.BaseRequestUrl);

            string responseAsString = await rawResponse.Content.ReadAsStringAsync();

            return string.IsNullOrWhiteSpace(responseAsString)
                       ? null
                       : JsonConvert.DeserializeObject<CatApiResponse[]>(responseAsString).FirstOrDefault();
        }
    }
}