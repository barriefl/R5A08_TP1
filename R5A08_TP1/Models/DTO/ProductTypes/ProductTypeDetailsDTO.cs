
namespace R5A08_TP1.Models.DTO.ProductTypes
{
    public class ProductTypeDetailsDTO
    {
        public int IdProductType { get; set; }
        public string NameProductType { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ProductTypeDetailsDTO dTO &&
                   this.IdProductType == dTO.IdProductType &&
                   this.NameProductType == dTO.NameProductType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.IdProductType, this.NameProductType);
        }
    }
}
