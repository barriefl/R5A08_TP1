namespace R5A08_TP1.Models.DTO
{
    public class ProductDetailsDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public string? PhotoName { get; set; }
        public string? UriPhoto { get; set; }
        public int? Stock { get; set; }
        public bool Restocking { get; set; }
    }
}
