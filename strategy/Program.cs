using DesignPatterns.Strategy.Business.Models;
using DesignPatterns.Strategy.Business.Strategies.SalesTax;

var order = new Order
{
    ShippingDetails = new ShippingDetails
    {
        OriginCountry = "Sweden",
        DestinationCountry = "Sweden"
    }
    
};

order.LineItems.Add(new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m, ItemType.Literature), 1);
order.LineItems.Add(new Item("CONSULTING", "Building a website", 100m, ItemType.Service), 1);

if(order.ShippingDetails.DestinationCountry == "Sweden")
{
    order.SalesTaxStrategy = new SwedenSalesTaxStrategy();
}

if (order.ShippingDetails.DestinationCountry == "US")
{
    order.SalesTaxStrategy = new USASalesTaxStrategy();
}


Console.WriteLine(order.GetTax());