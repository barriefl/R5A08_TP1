using R5A08_TP1.Models.DTO;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Mapper
{
    public class ProductMapper : IMapper<Produit, ProductDto>
    {
        public Produit? FromDTO(ProductDto dto)
        {
            return new Produit
            {
                IdProduit = dto.Id,
                NomProduit = dto.Nom,
                TypeProduitNavigation = new TypeProduit { NomTypeProduit = dto.Type },
                MarqueNavigation = new Marque { NomMarque = dto.Marque }
                };
        }

        public ProductDto? FromEntity(Produit entity)
        {
            return new ProductDto
            {
                Id = entity.IdProduit,
                Nom = entity.NomProduit,
                Type = entity.TypeProduitNavigation.NomTypeProduit,
                Marque = entity.MarqueNavigation.NomMarque
            };
        }
    }
}
