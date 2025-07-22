using System;

abstract class Animal
{
    protected string name;

    public Animal(string name)
    {
        this.name = name;
        Console.WriteLine($"Animal constructor: {name}");
    }

    public void Sleep()
    {
        Console.WriteLine($"{name} is trying to sleep.");
    }

    public abstract void MakeSound(); // Must be overridden
}

class Dog : Animal
{
    public Dog(string name) : base(name) {}

    public override void MakeSound()
    {
        Console.WriteLine($"{name} says: Woof!");
    }
}

class Cat : Animal
{
    public Cat(string name) : base(name) {}

    public override void MakeSound()
    {
        Console.WriteLine($"{name} says: Meow!");
    }
}

class Program
{
    static void Main()
    {
        Dog d = new Dog("Rocky");
        d.Sleep();        // From abstract class
        d.MakeSound();    // Overridden in Dog

        Cat c = new Cat("dummy");
        c.Sleep();        // From abstract class
        c.MakeSound();    // Overridden in Cat
    }
}
