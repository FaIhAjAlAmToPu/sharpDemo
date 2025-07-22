using System;

interface IMachine
{
    void Start();
}

interface IAdvancedMachine : IMachine
{
    void Stop();
}

class Robot : IAdvancedMachine
{
    public void Start() => Console.WriteLine("Robot started.");
    public void Stop() => Console.WriteLine("Robot stopped.");
}

class Program
{
    static void Main()
    {
        Robot r = new Robot();
        r.Start();
        r.Stop();
    }
}
