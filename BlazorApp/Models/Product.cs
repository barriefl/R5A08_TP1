using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Models
{
    public class Product
    {
        public int IdProduit { get; set; }

        //[Column("marq_id")]
        //[ForeignKey(nameof(Marque.ProduitsAssociesMarques))]
        //public int? IdMarque { get; set; }

        //[Column("typr_id")]
        //[ForeignKey(nameof(TypeProduit.ProduitsAssociesTypesProduit))]
        //public int? IdTypeProduit { get; set; }

        public string NomProduit { get; set; } = null!;

        public string DescriptionProduit { get; set; } = null!;

        public string NomPhotoProduit { get; set; } = null!;

        public string UriPhotoProduit { get; set; } = null!;

        //[Column("prod_stockreel")]
        //public int StockReelProduit { get; set; }

        //[Column("prod_stockmin")]
        //public int StockMinProduit { get; set; }

        //[Column("prod_stockmax")]
        //public int StockMaxProduit { get; set; }

        //[ForeignKey(nameof(TypeProduit.IdTypeProduit))]
        //[InverseProperty(nameof(TypeProduit.ProduitsAssociesTypesProduit))]
        //public virtual TypeProduit? TypeProduitNavigation { get; set; } = null!;

        //[ForeignKey(nameof(Marque.IdMarque))]
        //[InverseProperty(nameof(Marque.ProduitsAssociesMarques))]
        //public virtual Marque? MarqueNavigation { get; set; } = null!;
    }
}
