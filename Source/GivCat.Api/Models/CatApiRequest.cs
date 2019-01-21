namespace GivCat.Api.Models
{
    public class CatApiRequest
    {
        public string BaseRequestUrl => "https://api.thecatapi.com/v1/images/search";

        public string Breed { get; set; }

        public string Category { get; set; }
    }
}