using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R5A08_TP1.Models.EntityFramework
{
    [Table("t_e_producttype_prty")]
    public partial class ProductType
    {
        [Key]
        [Column("prty_id")]
        public int IdProductType { get; set; }

        [Required(ErrorMessage = "Product type name is required.")]
        [StringLength(100, ErrorMessage = "Product type name cannot exceed 100 characters.")]
        [Column("prty_name")]
        public string NameProductType { get; set; } = null!;

        [InverseProperty(nameof(Product.ProductTypeNavigation))]
        public virtual ICollection<Product> RelatedProductsProductType { get; set; } = new List<Product>();
    }
}
