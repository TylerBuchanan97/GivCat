namespace GivCat.Api.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    public class CatApiResponse
    {
        public IEnumerable<JObject> Breeds { get; set; }

        public string Url { get; set; }
    }
}