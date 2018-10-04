using System.Collections.Generic;
using System.Threading.Tasks;
using CFT.Data.Models;

namespace CFT.App.Core.Interfaces
{
    public interface IDataStore<T> where T : BaseItem
    {
        string ApiBase { get; set; }

        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
