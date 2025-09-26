using BlazorApp.Models;
using BlazorApp.Services;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.ViewModels
{
    public partial class UpdateProductViewModel
    {
        private readonly IService<Product> _productService;

        public UpdateProductViewModel(IService<Product> productService)
        {
            _productService = productService;
        }

        [Required]
        public int IdProduct { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Product name is limited to 100 caracters.")]
        public string NameProduct { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string NameBrand { get; set; }

        [Required]
        public string NameProductType { get; set; }

        [Required]
        public string PhotoName { get; set; }

        [Required]
        public string UriPhoto { get; set; }

        [Required]
        public int RealStock { get; set; }

        [Required]
        public int MinStock { get; set; }

        [Required]
        public int MaxStock { get; set; }

        public async Task LoadProductAsync(int id)
        {
            await _productService.GetByIdAsync(id);
        }

        public async Task UpdateProductAsync(int idProduct)
        {
            Product product = ViewModelToModel();

            await _productService.UpdateAsync(product);
        }

        public Product ViewModelToModel()
        {
            return new Product
            {
                IdProduct = IdProduct,
                NameProduct = NameProduct,
                DescriptionProduct = Description,
                NameBrand = NameBrand,
                NameProductType = NameProductType,
                PhotoNameProduct = PhotoName,
                UriPhotoProduct = UriPhoto,
                RealStock = RealStock,
                MinStock = MinStock,
                MaxStock = MaxStock
            };
        }

        public void ClearForm()
        {
            NameProduct = string.Empty;
            NameBrand = string.Empty;
            NameProductType = string.Empty;
            Description = string.Empty;
            PhotoName = string.Empty;
            UriPhoto = string.Empty;
            RealStock = 0;
            MinStock = 0;
            MaxStock = 0;
        }
    }
}
