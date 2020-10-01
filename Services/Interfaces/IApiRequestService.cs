using System.Threading.Tasks;

namespace Peitho.Services
{
    public interface IApiRequestService
    {
        /// <summary>
        /// Handles a GET request to an external API and attempts to deserialize the
        /// response to the type <typeparam name="TEntity"></typeparam>.
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Task<TEntity> HandleGetRequest<TEntity>(string requestUrl);
    }
}