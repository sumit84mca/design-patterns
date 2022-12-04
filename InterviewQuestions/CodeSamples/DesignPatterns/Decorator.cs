

using System;

namespace DecoratorPattern
{
    /// <summary>
    /// Decorator Interface
    /// </summary>
    public interface IExpression
    {
        int Execute();
    }

    /// <summary>
    /// Concret class
    /// </summary>
    public class NumberExpression : IExpression
    {
        public NumberExpression(int number)
        {
            Number = number;
        }

        public int Number { get; }

        public int Execute()
        {
            return Number;
        }
    }

    /// <summary>
    /// Add Expression Decorator
    /// </summary>
    public class AddExpression : IExpression
    {
        public AddExpression(IExpression firstExpression, IExpression secondExpression)
        {
            FirstExpression = firstExpression;
            SecondExpression = secondExpression;
        }

        public IExpression FirstExpression { get; }
        public IExpression SecondExpression { get; }

        public int Execute()
        {
            return FirstExpression.Execute() + SecondExpression.Execute();
        }
    }

    /// <summary>
    /// Square Expression Decorator
    /// </summary>
    public class SquareExpression : IExpression
    {
        public SquareExpression(IExpression expression)
        {
            Expression = expression;
        }

        public IExpression Expression { get; }

        public int Execute()
        {
            return Expression.Execute() * Expression.Execute();
        }
    }

    public class DecoratorClient
    {
        public static void DecoratorMain()
        {
            IExpression numberExpression1 = new NumberExpression(8);
            IExpression numberExpression2 = new NumberExpression(5);

            Console.WriteLine($"Executing numberExpression1 {numberExpression1.Execute()}");
            Console.WriteLine($"Executing numberExpression2 {numberExpression2.Execute()}");

            IExpression addExpression = new AddExpression(numberExpression1, numberExpression2);

            Console.WriteLine($"Executing addExpression {addExpression.Execute()}");

            IExpression squareExpression = new SquareExpression(addExpression);

            Console.WriteLine($"Executing squareExpression {squareExpression.Execute()}");

            Console.ReadLine();

        }
    }

}

