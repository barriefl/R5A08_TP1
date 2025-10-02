

namespace R5A08_TP1.Models.EntityFramework
{
    public partial class Brand
    {
        public Brand()
        {
        }

        public Brand (int idBrand, string nameBrand, ICollection<Product> relatedProductsBrand)
        {
            IdBrand = idBrand;
            NameBrand = nameBrand;
            RelatedProductsBrand = relatedProductsBrand;
        }

        public override bool Equals(object? obj)
        {
            return obj is Brand brand &&
                   this.IdBrand == brand.IdBrand &&
                   this.NameBrand == brand.NameBrand &&
                   EqualityComparer<ICollection<Product>>.Default.Equals(this.RelatedProductsBrand, brand.RelatedProductsBrand);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.IdBrand, this.NameBrand, this.RelatedProductsBrand);
        }
    }
}
