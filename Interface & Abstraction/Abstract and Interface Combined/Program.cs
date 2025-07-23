using System;

interface IDriveable
{
    void Drive();
}

abstract class Vehicle
{
    public abstract void Start();
    public void FuelUp() => Console.WriteLine("Vehicle is fueled up.");
}

class Car : Vehicle, IDriveable
{
    public override void Start() => Console.WriteLine("Car started with key.");
    public void Drive() => Console.WriteLine("Car is driving...");
}

class Program
{
    static void Main()
    {
        Car car = new Car();
        car.FuelUp();  // from abstract base
        car.Start();   // overridden abstract
        car.Drive();   // interface
    }
}