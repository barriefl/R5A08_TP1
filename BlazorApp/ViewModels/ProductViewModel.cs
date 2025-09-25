using BlazorApp.Models;
using BlazorApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazorApp.ViewModels
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly IService<Product> _productService;

        public ProductViewModel(IService<Product> productService)
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
