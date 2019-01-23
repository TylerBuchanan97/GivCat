namespace GivCat.Bot.Initialization
{
    using System.Threading.Tasks;

    public interface IBotInitializer
    {
        Task InitializeGivCatBot();
    }
}