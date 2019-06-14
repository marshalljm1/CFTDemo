using System.Collections.Generic;
using System.Threading.Tasks;

namespace CFT.Promotions.Core.Interfaces
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        string ApiBase { get; set; }
    }
}
