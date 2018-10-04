using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CFT.App.Core.Interfaces;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CFT.App.Core.Services
{
    public class DataStore<T> : IDataStore<T> where T : BaseItem
    {
        private readonly HttpClient _client;
        private IEnumerable<T> _items;
        public string ApiBase { get; set; }

        public DataStore(string baseUrl)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri($"{baseUrl}/")
            };

            _items = new List<T>();

            ApiBase = "trips";
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                try
                {
                    var json = await _client.GetStringAsync($"api/{ApiBase}");
                    _items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(json));
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }

            return _items;
        }

        public async Task<T> GetItemAsync(int id)
        {
            try
            {
                var json = await _client.GetStringAsync($"api/{ApiBase}/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(json));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return null;
            }
        }

        

        public async Task<bool> AddItemAsync(T item)
        {
            if (item == null)
                return false;

            try
            {
                var serializedItem = JsonConvert.SerializeObject(item);

                var response = await _client.PostAsync($"api/{ApiBase}", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            if (item?.Id == null)
                return false;

            try
            {
                var serializedItem = JsonConvert.SerializeObject(item);
                var buffer = Encoding.UTF8.GetBytes(serializedItem);
                var byteContent = new ByteArrayContent(buffer);

                var response = await _client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
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
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }
        }
    }
}