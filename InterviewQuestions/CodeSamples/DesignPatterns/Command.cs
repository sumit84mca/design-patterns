
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandPattern
{
    internal interface ICommand
    {
        void Execute();
        void Undo();
    }
    /// <summary>
    /// This object knows everything about the Product and price which will be increase. It also knows about the method which will change the product price
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


    internal class ProductCommandManager
    {
        private Stack<ICommand> commands;

        public ProductCommandManager()
        {
            this.commands = new Stack<ICommand>();
        }

        public void Execute(ICommand command)
        {
            if (command != null)
            {
                commands.Push(command);
                command.Execute();
            }
        }
        public void Undo(int numberOfCommands)
        {
            if (numberOfCommands == 0)
            {
                numberOfCommands = commands.Count;
            }
            for (int i = 0; i < numberOfCommands; i++)
            {
                commands.Pop().Undo();
            }
        }
    }

    //Receiver
    internal class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }
        public void IncreasePrice(int amount)
        {
            Price += amount;
            Console.WriteLine($"The price for the {Name} has been increased by {amount}$.");
        }
        public void DecreasePrice(int amount)
        {
            if (amount < Price)
            {
                Price -= amount;
                Console.WriteLine($"The price for the {Name} has been decreased by {amount}$.");
            }
        }
        public override string ToString() => $"Current price for the {Name} product is {Price}$.";
    }


    // Client Application
    public static class CommandClient
    {

        public static void CommandMain()
        {
            //Invoker
            ProductCommandManager commandManager = new ProductCommandManager();

            //Receiver
            Product product = new Product("phone", 30000);

            commandManager.Execute(/*Command Object*/new IncreasePriceCommand(product, 1000));
            Console.WriteLine(product);
            commandManager.Execute(new IncreasePriceCommand(product, 500));
            Console.WriteLine(product);
            commandManager.Execute(new DecreasePriceCommand(product, 400));
            Console.WriteLine(product);

            commandManager.Undo(2);
            Console.WriteLine(product);

            Console.ReadLine();
        }
    }

}