using R5A08_TP1.Models.DTO;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Mapper
{
    public class ProductMapper : IMapper<Product, ProductDTO>
    {
        public Product? FromDTO(ProductDTO dto)
        {
            return new Product
            {
                IdProduit = dto.Id,
                NomProduit = dto.Nom,
                TypeProduitNavigation = new ProductType { NomTypeProduit = dto.Type },
                MarqueNavigation = new Brand { NomMarque = dto.Marque }
                };
        }

        public ProductDTO? FromEntity(Product entity)
        {
            return new ProductDTO
            {
                Id = entity.IdProduit,
                Nom = entity.NomProduit,
                Type = entity.TypeProduitNavigation.NomTypeProduit,
                Marque = entity.MarqueNavigation.NomMarque
            };
        }
    }
}
