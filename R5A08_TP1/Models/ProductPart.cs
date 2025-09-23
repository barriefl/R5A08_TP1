
namespace R5A08_TP1.Models.EntityFramework
{
    public partial class Product
    {
        public Product()
        {
        }

        public Product(int idProduct, int? idBrand, int? idProductType, string nameProduct, string descriptionProduct, string photoNameProduct, string uriPhotoProduct, int realStock, int minStock, int maxStock, ProductType? productTypeNavigation, Brand? brandNavigation)
        {
            IdProduct = idProduct;
            IdBrand = idBrand;
            IdProductType = idProductType;
            NameProduct = nameProduct;
            DescriptionProduct = descriptionProduct;
            PhotoNameProduct = photoNameProduct;
            UriPhotoProduct = uriPhotoProduct;
            RealStock = realStock;
            MinStock = minStock;
            MaxStock = maxStock;
            ProductTypeNavigation = productTypeNavigation;
            BrandNavigation = brandNavigation;
        }

        public override bool Equals(object? obj)
        {
            return obj is Product product &&
                   IdProduct == product.IdProduct &&
                   IdBrand == product.IdBrand &&
                   IdProductType == product.IdProductType &&
                   NameProduct == product.NameProduct &&
                   DescriptionProduct == product.DescriptionProduct &&
                   PhotoNameProduct == product.PhotoNameProduct &&
                   UriPhotoProduct == product.UriPhotoProduct &&
                   RealStock == product.RealStock &&
                   MinStock == product.MinStock &&
                   MaxStock == product.MaxStock &&
                   EqualityComparer<ProductType?>.Default.Equals(ProductTypeNavigation, product.ProductTypeNavigation) &&
                   EqualityComparer<Brand?>.Default.Equals(BrandNavigation, product.BrandNavigation);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(IdProduct);
            hash.Add(IdBrand);
            hash.Add(IdProductType);
            hash.Add(NameProduct);
            hash.Add(DescriptionProduct);
            hash.Add(PhotoNameProduct);
            hash.Add(UriPhotoProduct);
            hash.Add(RealStock);
            hash.Add(MinStock);
            hash.Add(MaxStock);
            hash.Add(ProductTypeNavigation);
            hash.Add(BrandNavigation);
            return hash.ToHashCode();
        }
    }
}
