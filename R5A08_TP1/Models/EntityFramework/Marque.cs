using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R5A08_TP1.Models.EntityFramework
{
    [Table("t_e_marque_marq")]
    public partial class Marque
    {
        [Key]
        [Column("marq_id")]
        public int IdMarque { get; set; }

        [Column("marq_nom")]
        public string NomMarque { get; set; } = null!;

        [InverseProperty(nameof(Produit.MarqueNavigation))]
        public virtual ICollection<Produit> ProduitsAssociesMarques { get; set; } = new List<Produit>();
    }
}
