namespace R5A08_TP1.Models.BrandPolicy
{
    public class PrecommandedPolicy : Policy
    {
        public PrecommandedPolicy(int realStock, int stockMin, int stockMax, bool isDisponible) : base(realStock, stockMin, stockMax, isDisponible)
        {
        }

        public override void BrandHasPolicy()
        {
            if (RealStock < StockMin) 
            {
                IsDisponible = true;
            }
            else if (RealStock > StockMax) 
            {
                IsDisponible = true;
            }
        }
    }
}
