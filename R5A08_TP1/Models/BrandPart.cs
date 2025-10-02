
namespace R5A08_TP1.Models.EntityFramework
{
    public partial class Brand
    {
        public Brand()
        {
        }

        public Brand(int idBrand, string nameBrand)
        {
            IdBrand = idBrand;
            NameBrand = nameBrand;
        }

        public override bool Equals(object? obj)
        {
            return obj is Brand brand &&
                   IdBrand == brand.IdBrand &&
                   NameBrand == brand.NameBrand &&
                   EqualityComparer<ICollection<Product>>.Default.Equals(RelatedProductsBrand, brand.RelatedProductsBrand);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdBrand, NameBrand, RelatedProductsBrand);
        }
    }
}
