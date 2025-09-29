using BlazorApp.Models;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components;
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
            Product selectedProduct = await _productService.GetByIdAsync(id);
            IdProduct = selectedProduct.IdProduct;
            NameProduct = selectedProduct.NameProduct;
            Description = selectedProduct.DescriptionProduct;
            NameBrand = selectedProduct.NameBrand;
            NameProductType = selectedProduct.NameProductType;
            PhotoName = selectedProduct.PhotoNameProduct;
            UriPhoto = selectedProduct.UriPhotoProduct;
            RealStock = selectedProduct.RealStock;
            MinStock = selectedProduct.MinStock;
            MaxStock = selectedProduct.MaxStock;
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
