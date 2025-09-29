using BlazorApp.Models;
using BlazorApp.Services;
using BlazorBootstrap;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.ViewModels
{
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly IService<Product> _productService;

        public ProductsViewModel(IService<Product> productService)
        {
            _productService = productService;
        }

        public bool IsLoading { get; set; }
        public IEnumerable<Product> Products = Enumerable.Empty<Product>();

        public async Task LoadDataAsync()
        {
            IsLoading = true;

            try
            {
                Products = await _productService.GetAllAsync();
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try 
            {
                await _productService.DeleteAsync(id);
                await LoadDataAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
