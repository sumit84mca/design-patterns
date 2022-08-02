

// Client Application
using DesignPatterns.Command;
using DesignPatterns.Command.Command;


//Invoker
ProductCommandManager commandManager = new ProductCommandManager();

//Receiver
Product product = new Product("phone",30000);

commandManager.Execute(/*Command Object*/new IncreasePriceCommand(product, 1000));
Console.WriteLine(product);
commandManager.Execute(new IncreasePriceCommand(product, 500));
Console.WriteLine(product);
commandManager.Execute(new DecreasePriceCommand(product, 400));
Console.WriteLine(product);

commandManager.Undo(2);
Console.WriteLine(product);

Console.ReadLine();
