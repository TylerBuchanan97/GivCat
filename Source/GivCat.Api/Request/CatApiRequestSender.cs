namespace GivCat.Api.Request
{
    using System;
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
            string requestUrl = BuildRequestUrl(request);

            HttpResponseMessage rawResponse = await new HttpClient().GetAsync(requestUrl);

            string responseAsString = await rawResponse.Content.ReadAsStringAsync();

            return string.IsNullOrWhiteSpace(responseAsString)
                       ? null
                       : JsonConvert.DeserializeObject<CatApiResponse[]>(responseAsString).FirstOrDefault();
        }

        private string BuildRequestUrl(CatApiRequest request)
        {
            string mimeTypes = string.Empty;

            foreach (string parameter in request.Parameters)
            {
                if (Enum.TryParse(parameter, true, out SupportedFileType fileType))
                {
                    switch (fileType)
                    {
                        case SupportedFileType.Gif:
                            mimeTypes = "gif";
                            break;
                        case SupportedFileType.Image:
                            mimeTypes = "jpg,png";
                            break;
                    }
                }
            }

            return $"{request.BaseRequestUrl}?mime_types={mimeTypes}";
        }
    }
}