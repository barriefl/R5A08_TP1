using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R5A08_TP1.Models.EntityFramework
{
    [Table("t_e_brand_bran")]
    public partial class Brand
    {
        [Key]
        [Column("bran_id")]
        public int IdBrand { get; set; }

        [Column("bran_name")]
        public string NameBrand { get; set; } = null!;

        [InverseProperty(nameof(Product.BrandNavigation))]
        public virtual ICollection<Product> RelatedProductsBrand { get; set; } = new List<Product>();
    }
}
