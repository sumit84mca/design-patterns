using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyPattern
{

    public class Student
    {
        public string Name { get; }
        public int Age { get; }
        public Student(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }
    }

    public interface IMyCollection<T>
    {
        void Add(T item);
        void Remove(T item);
        void SetSortStreatgy(SortStrategy strategy);
        void Sort();
    }

    public class MyStudentCollection : IMyCollection<Student>
    {
        private List<Student> students;
        private SortStrategy sortStrategy;
        public MyStudentCollection()
        {
            students = new List<Student>();
        }

        public void Add(Student student)
        {
            students.Add(student);
        }

        public void Remove(Student student)
        {
            students.Remove(student);
        }
        public void SetSortStreatgy(SortStrategy strategy)
        {
            sortStrategy = strategy;
        }
        public void Sort()
        {
            sortStrategy.Sort(students);
        }
    }


    /// <summary>
    /// Strategy Design Pattern
    /// </summary>

    public class StreatgyClient
    {
        public static void StreatgyMain(string[] args)
        {
            MyStudentCollection students = new MyStudentCollection();

            students.Add(new Student("Amit", 14));
            students.Add(new Student("Raj", 13));
            students.Add(new Student("Sumit", 15));
            students.Add(new Student("Prakash", 9));
            students.Add(new Student("Ujjwal", 10));
            students.Add(new Student("Suman", 15));

            students.SetSortStreatgy(new SortByName());
            students.Sort();

            students.SetSortStreatgy(new SortByAge());
            students.Sort();

            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Strategy' abstract class
    /// </summary>

    public abstract class SortStrategy
    {
        public abstract void Sort(List<Student> list);
    }

    /// <summary>
    /// A 'ConcreteStrategy' class
    /// </summary>

    public class SortByName : SortStrategy
    {
        public override void Sort(List<Student> list)
        {
            list.OrderBy(stu => stu.Name);
            Console.WriteLine("Sorted by name ");
        }
    }

    /// <summary>
    /// A 'ConcreteStrategy' class
    /// </summary>

    public class SortByAge : SortStrategy
    {
        public override void Sort(List<Student> list)
        {
            list.OrderBy(s => s.Age);
            Console.WriteLine("ShellSorted list ");
        }
    }
}
