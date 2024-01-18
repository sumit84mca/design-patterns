namespace ConstructorCalling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var obj= new Child("this");

            Console.WriteLine("Calling completed!");
            Console.ReadLine();

            /* Below is the result of calling. It can be seen the order of calling is
             * 1. Static of Child
             * 2. Static of Parent
             * 3. Normal of Parent
             * 4. Normal of Child

            /*
             * Hello, World!
             * Writing from static constructor of Child
             * Writing from static constructor of Parent
             * Writing from normal parameterized constructor of Parent
             * Writing from normal parameterized constructor of Child
             * Calling completed!
             */
        }
    }
}