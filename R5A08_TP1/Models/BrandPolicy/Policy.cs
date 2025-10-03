using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.BrandPolicy
{
    public abstract class Policy
    {
        public Product Product { get; set; }
        public int RealStock { get; private set; }
        public int StockMin { get; private set; }
        public int StockMax { get; private set; }
        public bool IsDisponible { get; set; }

        protected Policy(Product product)
        {
            RealStock = realStock;
            StockMin = stockMin;
            StockMax = stockMax;
            IsDisponible = isDisponible;
        }

        public void BrandHasStrictPolicy()
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

        public void BrandHasPrecommandedPolicy()
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

        public void BrandHasCriticPolicy()
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
