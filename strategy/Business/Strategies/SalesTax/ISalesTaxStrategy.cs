using DesignPatterns.Strategy.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Strategy.Business.Strategies.SalesTax
{
    internal interface ISalesTaxStrategy
    {
        public decimal GetTaxFor(Order order);
    }
}