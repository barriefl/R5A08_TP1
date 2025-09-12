using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R5A08_TP1.Models.EntityFramework
{
    [Table("t_e_typeproduit_typr")]
    public class TypeProduit
    {
        [Key]
        [Column("typr_id")]
        public int IdTypeProduit { get; set; }

        [Column("typr_nom")]
        public string NomMarque { get; set; } = null!;

        [InverseProperty(nameof(Produit.TypeProduitNavigation))]
        public virtual ICollection<Produit> ProduitsAssociesTypesProduit { get; set; } = new List<Produit>();
    }
}
