
namespace R5A08_TP1.Models.DTO.ProductTypes
{
    public class ProductTypeDTO
    {
        public string NameProductType { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ProductTypeDTO dTO &&
                   this.NameProductType == dTO.NameProductType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.NameProductType);
        }
    }
}
