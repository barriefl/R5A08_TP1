
namespace R5A08_TP1.Models.DTO
{
    public class ProductDetailsDTO
    {
        public int IdProduct { get; set; }
        public string? NameProduct { get; set; }
        public string? NameProductType { get; set; }
        public string? NameBrand { get; set; }
        public string? DescriptionProduct { get; set; }
        public string? PhotoNameProduct { get; set; }
        public string? UriPhotoProduct { get; set; }
        public int? RealStock { get; set; }
        public bool Restocking { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ProductDetailsDTO dTO &&
                   IdProduct == dTO.IdProduct &&
                   NameProduct == dTO.NameProduct &&
                   NameProductType == dTO.NameProductType &&
                   NameBrand == dTO.NameBrand &&
                   DescriptionProduct == dTO.DescriptionProduct &&
                   PhotoNameProduct == dTO.PhotoNameProduct &&
                   UriPhotoProduct == dTO.UriPhotoProduct &&
                   RealStock == dTO.RealStock &&
                   Restocking == dTO.Restocking;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(IdProduct);
            hash.Add(NameProduct);
            hash.Add(NameProductType);
            hash.Add(NameBrand);
            hash.Add(DescriptionProduct);
            hash.Add(PhotoNameProduct);
            hash.Add(UriPhotoProduct);
            hash.Add(RealStock);
            hash.Add(Restocking);
            return hash.ToHashCode();
        }
    }
}
