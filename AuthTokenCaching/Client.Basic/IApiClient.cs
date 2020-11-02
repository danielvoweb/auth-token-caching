using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Basic
{
    public interface IApiClient<T>
    {
        
        Task<IEnumerable<T>> GetAllAsync();
    }
}