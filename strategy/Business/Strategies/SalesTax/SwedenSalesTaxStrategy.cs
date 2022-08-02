using DesignPatterns.Strategy.Business.Models;

namespace DesignPatterns.Strategy.Business.Strategies.SalesTax
{
    internal class SwedenSalesTaxStrategy : ISalesTaxStrategy
    {
        public decimal GetTaxFor(Order order)
        {
            if (order.ShippingDetails.DestinationCountry.ToLowerInvariant() == order.ShippingDetails.OriginCountry.ToLowerInvariant())
            {
                decimal totalTax = 0m;
                foreach (var item in order.LineItems)
                {
                    switch (item.Key.ItemType)
                    {
                        case ItemType.Food:
                            totalTax += (item.Key.Price * 0.06m) * item.Value;
                            break;

                        case ItemType.Literature:
                            totalTax += (item.Key.Price * 0.08m) * item.Value;
                            break;

                        case ItemType.Service:
                        case ItemType.Hardware:
                            totalTax += (item.Key.Price * 0.25m) * item.Value;
                            break;
                    }
                }
                return totalTax;
            }
            return 0;
        }
    }
}