using System;

abstract class Person
{
    protected string name;

    public Person(string name)
    {
        this.name = name;
        Console.WriteLine($"Person constructor: {name}");
    }

    public abstract void Work();
}

class Student : Person
{
    private string studentId;

    public Student(string name, string id) : base(name)
    {
        studentId = id;
    }

    public override void Work()
    {
        Console.WriteLine($"{name} is studying. ID: {studentId}");
    }
}

class Program
{
    static void Main()
    {
        Student s = new Student("Alice", "ST123");
        s.Work();
    }
}
