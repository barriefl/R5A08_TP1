using BlazorApp.Models;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class WebService : IService<Product>
    {
        private readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:5011/api/")
        };

        public async Task AddAsync(Product product)
        {
            await httpClient.PostAsJsonAsync<Product>("products", product);
        }

        public async Task DeleteAsync(int id)
        {
            await httpClient.DeleteAsync($"product/{id}");
        }

        public async Task<IEnumerable<Product>?> GetAllAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Product>?>("products");
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<Product?>($"product/{id}");
        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            var response = await httpClient.PostAsJsonAsync("products/search", name);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Product>();
        }

        public async Task UpdateAsync(Product updatedEntity)
        {
            await httpClient.PutAsJsonAsync<Product>($"products/{updatedEntity.IdProduit}", updatedEntity);
        }
    }
}
