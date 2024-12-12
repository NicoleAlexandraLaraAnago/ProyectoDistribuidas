using SLC;
using Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NWindProxyService
{
    public class Proxy : IProductService
    {
        private string BaseAddress = "http://localhost:55488";

        // Método para enviar peticiones POST
        private async Task<T> SendPost<T, PostData>(string requestURI, PostData data)
        {
            T Result = default(T);
            using (var Client = new HttpClient())
            {
                try
                {
                    requestURI = BaseAddress + requestURI; // URL absoluta
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var JSONData = JsonConvert.SerializeObject(data);
                    HttpResponseMessage Response = await Client.PostAsync(requestURI, new StringContent(JSONData, Encoding.UTF8, "application/json"));
                    var ResultWebAPI = await Response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<T>(ResultWebAPI);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en SendPost: {ex.Message}");
                }
            }
            return Result;
        }

        // Método para enviar peticiones GET
        private async Task<T> SendGet<T>(string requestURI)
        {
            T Result = default(T);
            using (var Client = new HttpClient())
            {
                try
                {
                    requestURI = BaseAddress + requestURI; // URL absoluta
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var ResultJSON = await Client.GetStringAsync(requestURI);
                    Result = JsonConvert.DeserializeObject<T>(ResultJSON);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en SendGet: {ex.Message}");
                }
            }
            return Result;
        }

        // Implementación de métodos de IProductService
        public async Task<Products> CreateAsync(Products newProduct)
        {
            return await SendPost<Products, Products>("/api/product/create", newProduct);
        }

        public Products Create(Products newProduct)
        {
            Products Result = null;
            Task.Run(async () => Result = await CreateAsync(newProduct)).Wait();
            return Result;
        }

        public async Task<Products> RetrieveAsync(int id)
        {
            return await SendGet<Products>($"/api/product/retrieve/{id}");
        }

        public Products Retrieve(int id)
        {
            Products Result = null;
            Task.Run(async () => Result = await RetrieveAsync(id)).Wait();
            return Result;
        }

        public async Task<bool> UpdateAsync(Products productToUpdate)
        {
            return await SendPost<bool, Products>("/api/product/update", productToUpdate);
        }

        public bool Update(Products productToUpdate)
        {
            bool Result = false;
            Task.Run(async () => Result = await UpdateAsync(productToUpdate)).Wait();
            return Result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await SendGet<bool>($"/api/product/delete/{id}");
        }

        public bool Delete(int id)
        {
            bool Result = false;
            Task.Run(async () => Result = await DeleteAsync(id)).Wait();
            return Result;
        }

        public async Task<List<Products>> GetAllProductsAsync()
        {
            return await SendGet<List<Products>>("/api/product/getall");
        }

        public List<Products> GetAllProducts()
        {
            List<Products> Result = null;
            Task.Run(async () => Result = await GetAllProductsAsync()).Wait();
            return Result;
        }

        public async Task<List<Products>> FilterProductsByCategoryIDAsync(int id)
        {
            return await SendGet<List<Products>>($"/api/product/filterbycategory/{id}");
        }

        public List<Products> FilterProductsByCategoryID(int id)
        {
            List<Products> Result = null;
            Task.Run(async () => Result = await FilterProductsByCategoryIDAsync(id)).Wait();
            return Result;
        }

        // Métodos para manejar categorías
        public async Task<Categories> CreateCategoryAsync(Categories newCategory)
        {
            return await SendPost<Categories, Categories>("/api/category/create", newCategory);
        }

        public Categories CreateCategory(Categories newCategory)
        {
            Categories Result = null;
            Task.Run(async () => Result = await CreateCategoryAsync(newCategory)).Wait();
            return Result;
        }
    }
}
