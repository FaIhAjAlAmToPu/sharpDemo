abstract class Report
{
    public void Title() => Console.WriteLine("Annual Report");
    public abstract void Generate();
}

class SalesReport : Report
{
    public override void Generate() => Console.WriteLine("Sales data report generated.");
}

class Program
{
    static void Main()
    {
        SalesReport sr = new SalesReport();
        sr.Title();
        sr.Generate();
    }
}
