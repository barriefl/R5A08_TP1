namespace R5A08_TP1.Models.BrandPolicy
{
    public class CriticPolicy : Policy
    {
        public CriticPolicy(int realStock, int stockMin, int stockMax, bool isDisponible) : base(realStock, stockMin, stockMax, isDisponible)
        {
        }

        public override void BrandHasPolicy()
        {
            if (RealStock < StockMin)
            {
                IsDisponible = false;
            }
            else if (RealStock > StockMax)
            {
                IsDisponible = false;
            }
        }
    }
}
