namespace R5A08_TP1.Models.DTO.Products
{
    public class CreateProductDTO
    {
        public string NameProduct { get; set; }
        public string DescriptionProduct { get; set; }
        public string NameBrand { get; set; }
        public string NameProductType { get; set; }
        public string PhotoNameProduct { get; set; }
        public string UriPhotoProduct { get; set; }
        public int RealStock { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CreateProductDTO dTO &&
                   NameProduct == dTO.NameProduct &&
                   DescriptionProduct == dTO.DescriptionProduct &&
                   NameBrand == dTO.NameBrand &&
                   NameProductType == dTO.NameProductType &&
                   PhotoNameProduct == dTO.PhotoNameProduct &&
                   UriPhotoProduct == dTO.UriPhotoProduct &&
                   RealStock == dTO.RealStock &&
                   MinStock == dTO.MinStock &&
                   MaxStock == dTO.MaxStock;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(NameProduct);
            hash.Add(DescriptionProduct);
            hash.Add(NameBrand);
            hash.Add(NameProductType);
            hash.Add(PhotoNameProduct);
            hash.Add(UriPhotoProduct);
            hash.Add(RealStock);
            hash.Add(MinStock);
            hash.Add(MaxStock);
            return hash.ToHashCode();
        }
    }
}
