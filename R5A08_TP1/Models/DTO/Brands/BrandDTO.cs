
namespace R5A08_TP1.Models.DTO.Brands
{
    public class BrandDTO
    {
        public string NameBrand { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BrandDTO dTO &&
                   this.NameBrand == dTO.NameBrand;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.NameBrand);
        }
    }
}
