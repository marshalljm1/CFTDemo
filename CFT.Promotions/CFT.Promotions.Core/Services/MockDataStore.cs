using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFT.Promotions.Core.Interfaces;
using CFT.Promotions.Core.Models;

namespace CFT.Promotions.Core.Services
{
    public class MockDataStore : IDataStore<BaseItem>
    {
        readonly List<BaseItem> _items;

        public MockDataStore()
        {
            _items = new List<BaseItem>();
            var mockItems = new List<BaseItem>
            {
                new BaseItem { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new BaseItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new BaseItem { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new BaseItem { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new BaseItem { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new BaseItem { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." },
            };

            foreach (var item in mockItems)
            {
                _items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(BaseItem item)
        {
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(BaseItem item)
        {
            var oldItem = _items.FirstOrDefault(arg => arg.Id == item.Id);
            _items.Remove(oldItem);
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _items.FirstOrDefault(arg => arg.Id == id);
            _items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<BaseItem> GetItemAsync(string id)
        {
            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<BaseItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_items);
        }
    }
}