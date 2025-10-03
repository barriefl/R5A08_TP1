namespace R5A08_TP1.Models.BrandPolicy
{
    public class StrictPolicy : Policy
    {
        public StrictPolicy(int realStock, int stockMin, int stockMax, bool isDisponible) : base(realStock, stockMin, stockMax, isDisponible)
        {
        }

        public override void BrandHasPolicy()
        {
            if (StockMin <= RealStock && RealStock <= StockMax)
            {
                IsDisponible = true;
            }
            else
            {
                IsDisponible = false;
            }
        }
    }
}
