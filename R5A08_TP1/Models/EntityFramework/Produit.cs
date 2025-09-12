using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace R5A08_TP1.Models.EntityFramework
{
    [Table("t_e_produit_prod")]
    public partial class Produit
    {
        [Key]
        [Column("prod_id")]
        public int IdProduit { get; set; }

        [Column("marq_id")]
        [ForeignKey(nameof(Marque.ProduitsAssociesMarques))]
        public int? IdMarque { get; set; }

        [Column("typr_id")]
        [ForeignKey(nameof(TypeProduit.ProduitsAssociesTypesProduit))]
        public int? IdTypeProduit { get; set; }

        [Column("prod_nomproduit")]
        public string NomProduit { get; set; }

        [Column("prod_description")]
        public string DescriptionProduit { get; set; }

        [Column("prod_nomphoto")]
        public string NomPhotoProduit { get; set; }

        [Column("prod_uriphoto")]
        public string UriPhotoProduit { get; set; }

        [Column("prod_stockreel")]
        public int StockReelProduit { get; set; }

        [Column("prod_stockmin")]
        public int StockMinProduit { get; set; }

        [Column("prod_stockmax")]
        public int StockMaxProduit { get; set; }

        [ForeignKey(nameof(TypeProduit.IdTypeProduit))]
        [InverseProperty(nameof(TypeProduit.ProduitsAssociesTypesProduit))]
        public virtual TypeProduit? TypeProduitNavigation { get; set; } = null!;

        [ForeignKey(nameof(Marque.IdMarque))]
        [InverseProperty(nameof(Marque.ProduitsAssociesMarques))]
        public virtual Marque? MarqueNavigation { get; set; } = null!;
    }
}
