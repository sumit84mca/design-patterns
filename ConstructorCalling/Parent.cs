using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorCalling
{
    internal class Parent
    {
        public Parent(string parameter)
        {
            Console.WriteLine($"Writing from normal parameterized constructor of {nameof(Parent)}");
        }
        static Parent()
        {
            Console.WriteLine($"Writing from static constructor of {nameof(Parent)}");
        }
        public Parent() {
            Console.WriteLine($"Writing from normal constructor of {nameof(Parent)}");
        }
    }

    internal class Child : Parent
    {
        public Child(string parameter):base(parameter)
        {
            Console.WriteLine($"Writing from normal parameterized constructor of {nameof(Child)}");
        }
        static Child()
        {
            Console.WriteLine($"Writing from static constructor of {nameof(Child)}");
        }
        public Child()
        {
            Console.WriteLine($"Writing from normal constructor of {nameof(Child)}");
        }
    }
}
