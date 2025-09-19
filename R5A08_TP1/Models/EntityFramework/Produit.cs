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

        [Column("prod_nameproduct")]
        public string NameProduct { get; set; }

        [Column("prod_description")]
        public string DescriptionProduct { get; set; }

        [Column("prod_photoname")]
        public string PhotoNameProduct { get; set; }

        [Column("prod_uriphoto")]
        public string UriPhotoProduct { get; set; }

        [Column("prod_realstock")]
        public int RealStock { get; set; }

        [Column("prod_minstock")]
        public int MinStock { get; set; }

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
