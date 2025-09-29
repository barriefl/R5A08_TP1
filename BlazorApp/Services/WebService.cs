using BlazorApp.Models;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class WebService : IService<Product>
    {
        private readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:5196/api/")
        };

        public async Task AddAsync(Product product)
        {
            await httpClient.PostAsJsonAsync<Product>("Products", product);
        }

        public async Task DeleteAsync(int id)
        {
            await httpClient.DeleteAsync($"Products/{id}");
        }

        public async Task<IEnumerable<Product>?> GetAllAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Product>?>("Products");
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<Product?>($"Products/GetById/{id}");
        }

        public async Task<IEnumerable<Product>?> GetByNameAsync(string name)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Product>?>($"Products/GetByName/{name}");
            //var response = await httpClient.PostAsJsonAsync("Products/GetByName", name);
            //response.EnsureSuccessStatusCode();

            //return await response.Content.ReadFromJsonAsync<IEnumerable<Product>?>();
        }

        public async Task UpdateAsync(Product updatedEntity)
        {
            await httpClient.PutAsJsonAsync<Product>($"Products/{updatedEntity.IdProduct}", updatedEntity);
        }
    }
}
