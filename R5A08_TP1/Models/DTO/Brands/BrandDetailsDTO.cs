
namespace R5A08_TP1.Models.DTO.Brands
{
    public class BrandDetailsDTO
    {
        public int IdBrand { get; set; }
        public string NameBrand { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BrandDetailsDTO dTO &&
                   this.IdBrand == dTO.IdBrand &&
                   this.NameBrand == dTO.NameBrand;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.IdBrand, this.NameBrand);
        }
    }
}
