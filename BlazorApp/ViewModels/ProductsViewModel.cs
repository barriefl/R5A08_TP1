using BlazorApp.Models;
using BlazorApp.Services;
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

        [ObservableProperty] private bool _isLoading;
        [ObservableProperty] private IEnumerable<Product> _products = Enumerable.Empty<Product>();

        public async Task LoadDataAsync()
        {
            IsLoading = true;

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2)); // simulate loading
                Products = await _productService.GetAllAsync();
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
