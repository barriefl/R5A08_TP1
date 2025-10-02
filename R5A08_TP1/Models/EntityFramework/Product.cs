using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace R5A08_TP1.Models.EntityFramework
{
    [Table("t_e_product_prod")]
    public partial class Product
    {
        [Key]
        [Column("prod_id")]
        public int IdProduct { get; set; }

        [Column("bran_id")]
        [ForeignKey(nameof(Brand.RelatedProductsBrand))]
        public int? IdBrand { get; set; }

        [Column("prty_id")]
        [ForeignKey(nameof(ProductType.RelatedProductsProductType))]
        public int? IdProductType { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        [Column("prod_nameproduct")]
        public string NameProduct { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        [Column("prod_description")]
        public string DescriptionProduct { get; set; }

        [Required(ErrorMessage = "Photo name is required.")]
        [StringLength(200, ErrorMessage = "Photo name cannot exceed 200 characters.")]
        [Column("prod_photoname")]
        public string PhotoNameProduct { get; set; }

        [Required(ErrorMessage = "URI photo is required.")]
        [StringLength(500, ErrorMessage = "URI photo cannot exceed 500 characters.")]
        //[Url(ErrorMessage = "URI photo must be a valid URL.")]
        [Column("prod_uriphoto")]
        public string UriPhotoProduct { get; set; }

        [Range(0, 100, ErrorMessage = "Real stock must be between zero or 100.")]
        [Column("prod_realstock")]
        public int RealStock { get; set; }

        [Range(0, 100, ErrorMessage = "Min stock must be between zero or 100.")]
        [Column("prod_minstock")]
        public int MinStock { get; set; }

        [Range(0, 100, ErrorMessage = "Max stock must be zero or 100.")]
        [Column("prod_maxstock")]
        public int MaxStock { get; set; }

        [ForeignKey(nameof(ProductType.IdProductType))]
        [InverseProperty(nameof(ProductType.RelatedProductsProductType))]
        public virtual ProductType? ProductTypeNavigation { get; set; } = null!;

        [ForeignKey(nameof(Brand.IdBrand))]
        [InverseProperty(nameof(Brand.RelatedProductsBrand))]
        public virtual Brand? BrandNavigation { get; set; } = null!;
    }
}
