namespace GivCat.Api.Common
{
    using System.Threading.Tasks;

    public interface IRequestSender<in TRequest, TResponse>
    {
        Task<TResponse> SendRequestAsync(TRequest request);
    }
}