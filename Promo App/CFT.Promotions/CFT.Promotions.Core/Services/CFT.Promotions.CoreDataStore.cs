using System;
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
        public string ApiBase { get; set; }

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
            try
            {
                if (forceRefresh)
                {
                    var json = await _client.GetStringAsync($"api/{ApiBase}");
                    _items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(json));
                }
            }
            catch (Exception ex)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            return _items;
        }

        public async Task<T> GetItemAsync(int id)
        {
            try { 
                var json = await _client.GetStringAsync($"api/{ApiBase}/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(json));
            }
            catch (Exception ex)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return default(T);
            }

        }

        public async Task<bool> AddItemAsync(T item)
        {
            try
            {
                if (item == null)
                    return false;

                var serializedItem = JsonConvert.SerializeObject(item);

                var response = await _client.PostAsync($"api/{ApiBase}", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            try
            {
                if (item?.Id == null)
                    return false;

                var serializedItem = JsonConvert.SerializeObject(item);
                var buffer = Encoding.UTF8.GetBytes(serializedItem);
                var byteContent = new ByteArrayContent(buffer);

                var response = await _client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/item/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }
        }
    }
}