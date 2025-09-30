

namespace R5A08_TP1.Models.DTO
{
    public class UpdateProductDTO
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public string NameBrand { get; set; }
        public string NameProductType { get; set; }
        public string DescriptionProduct { get; set; }
        public string PhotoNameProduct { get; set; }
        public string UriPhotoProduct { get; set; }
        public int RealStock { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is UpdateProductDTO dTO &&
                   IdProduct == dTO.IdProduct &&
                   NameProduct == dTO.NameProduct &&
                   NameBrand == dTO.NameBrand &&
                   NameProductType == dTO.NameProductType &&
                   DescriptionProduct == dTO.DescriptionProduct &&
                   PhotoNameProduct == dTO.PhotoNameProduct &&
                   UriPhotoProduct == dTO.UriPhotoProduct &&
                   RealStock == dTO.RealStock &&
                   MinStock == dTO.MinStock &&
                   MaxStock == dTO.MaxStock;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(IdProduct);
            hash.Add(NameProduct);
            hash.Add(NameBrand);
            hash.Add(NameProductType);
            hash.Add(DescriptionProduct);
            hash.Add(PhotoNameProduct);
            hash.Add(UriPhotoProduct);
            hash.Add(RealStock);
            hash.Add(MinStock);
            hash.Add(MaxStock);
            return hash.ToHashCode();
        }
    }
}
