using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Command.Command
{
    internal interface ICommand
    {
        void Execute();
        void Undo();
    }

    /// <summary>
    /// This object knows everything about the Product and price which will be increased.
    /// It also knows about the method which will change the product price
    /// </summary>
    internal class IncreasePriceCommand : ICommand
    {
        private readonly Product product;
        private readonly int amount;

        public IncreasePriceCommand(Product product, int amount)
        {
            this.product = product;
            this.amount = amount;
        }

        public void Execute()
        {
            product.IncreasePrice(amount);
        }
        public void Undo()
        {
            product.DecreasePrice(amount);
        }
    }

    internal class DecreasePriceCommand : ICommand
    {
        private readonly Product product;
        private readonly int amount;

        public DecreasePriceCommand(Product product, int amount)
        {
            this.product = product;
            this.amount = amount;
        }

        public void Execute()
        {
            product.DecreasePrice(amount);
        }
        public void Undo()
        {
            product.IncreasePrice(amount);
        }
    }
}
