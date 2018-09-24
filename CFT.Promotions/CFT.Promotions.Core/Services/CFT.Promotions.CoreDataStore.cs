﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CFT.Promotions.Core.Interfaces;
using CFT.Promotions.Core.Models;
using Newtonsoft.Json;

namespace CFT.Promotions.Core.Services
{
    public class DataStore<T> : IDataStore<T> where T : BaseItem
    {
        private readonly HttpClient _client;
        private IEnumerable<T> _items;

        public DataStore()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri($"{App.BackendUrl}/")
            };

            _items = new List<T>();
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                var json = await _client.GetStringAsync("api/item");
                _items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(json));
            }

            return _items;
        }

        public async Task<T> GetItemAsync(string id)
        {
            if (id != null)
            {
                var json = await _client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(json));
            }

            return default(T);
        }

        public async Task<bool> AddItemAsync(T item)
        {
            if (item == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await _client.PostAsync("api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            if (item?.Id == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await _client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var response = await _client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}