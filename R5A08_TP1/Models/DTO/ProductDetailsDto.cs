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
    }
}
