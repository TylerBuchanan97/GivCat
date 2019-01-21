namespace GivCat.Bot.Initialization
{
    using System;
    using System.Threading.Tasks;

    public interface IBotInitializer
    {
        Task InitializeGivCatBot(string botKey, IServiceProvider serviceProvider);
    }
}