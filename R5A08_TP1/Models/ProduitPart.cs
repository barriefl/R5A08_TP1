namespace R5A08_TP1.Models.EntityFramework
{
    public partial class Product
    {
        public Product()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is Product produit &&
                   IdProduit == produit.IdProduit &&
                   IdMarque == produit.IdMarque &&
                   IdTypeProduit == produit.IdTypeProduit &&
                   NomProduit == produit.NomProduit &&
                   DescriptionProduit == produit.DescriptionProduit &&
                   NomPhotoProduit == produit.NomPhotoProduit &&
                   UriPhotoProduit == produit.UriPhotoProduit &&
                   StockReelProduit == produit.StockReelProduit &&
                   StockMinProduit == produit.StockMinProduit &&
                   StockMaxProduit == produit.StockMaxProduit &&
                   EqualityComparer<ProductType?>.Default.Equals(TypeProduitNavigation, produit.TypeProduitNavigation) &&
                   EqualityComparer<Brand?>.Default.Equals(MarqueNavigation, produit.MarqueNavigation);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(IdProduit);
            hash.Add(IdMarque);
            hash.Add(IdTypeProduit);
            hash.Add(NomProduit);
            hash.Add(DescriptionProduit);
            hash.Add(NomPhotoProduit);
            hash.Add(UriPhotoProduit);
            hash.Add(StockReelProduit);
            hash.Add(StockMinProduit);
            hash.Add(StockMaxProduit);
            hash.Add(TypeProduitNavigation);
            hash.Add(MarqueNavigation);
            return hash.ToHashCode();
        }
    }
}
