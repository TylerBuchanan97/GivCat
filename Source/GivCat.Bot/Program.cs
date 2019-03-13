namespace GivCat.Bot
{
    using System;
    using System.Threading.Tasks;

    using GivCat.Bot.Initialization;
    using GivCat.Bot.Ioc;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main()
        {
            IServiceProvider serviceProvider = DependencyManagement.Provider;

            await serviceProvider.GetService<IBotInitializer>().InitializeGivCatBot();

            await Task.Delay(-1);
        }
    }
}