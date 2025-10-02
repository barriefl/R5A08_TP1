namespace R5A08_TP1.Models.DTO.Products
{
    public class ProductDTO
    {
        public int IdProduct { get; set; }
        public string? NameProduct { get; set; }
        public string? NameProductType { get; set; }
        public string? NameBrand { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ProductDTO dTO &&
                   IdProduct == dTO.IdProduct &&
                   NameProduct == dTO.NameProduct &&
                   NameProductType == dTO.NameProductType &&
                   NameBrand == dTO.NameBrand;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdProduct, NameProduct, NameProductType, NameBrand);
        }
    }
}
