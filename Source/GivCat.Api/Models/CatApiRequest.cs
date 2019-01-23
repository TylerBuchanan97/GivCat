namespace GivCat.Api.Models
{
    using System.Collections.Generic;

    public class CatApiRequest
    {
        public string BaseRequestUrl => "https://api.thecatapi.com/v1/images/search";

        public IEnumerable<string> Parameters { get; set; }
    }
}