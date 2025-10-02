

namespace R5A08_TP1.Models.EntityFramework
{
    public partial class ProductType
    {
        public ProductType()
        {
        }

        public ProductType(int idProductType, string nameProductType, ICollection<Product> relatedProductsProductType)
        {
            this.IdProductType = idProductType;
            this.NameProductType = nameProductType;
            this.RelatedProductsProductType = relatedProductsProductType;
        }

        public override bool Equals(object? obj)
        {
            return obj is ProductType type &&
                   this.IdProductType == type.IdProductType &&
                   this.NameProductType == type.NameProductType &&
                   EqualityComparer<ICollection<Product>>.Default.Equals(this.RelatedProductsProductType, type.RelatedProductsProductType);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.IdProductType, this.NameProductType, this.RelatedProductsProductType);
        }
    }
}
