interface IPlayable
{
    void Play();
}

abstract class Game
{
    public abstract void Start();
    public void CommonRule() => Console.WriteLine("Follow fair play rules.");
}

class Football : Game, IPlayable
{
    public override void Start() => Console.WriteLine("Football match started!");
    public void Play() => Console.WriteLine("Playing football...");
    
    public void ExtraTime() => Console.WriteLine("Playing extra time!");
}

class Program
{
    static void Main()
    {
        Football f = new Football();
        f.CommonRule();
        f.Start();
        f.Play();
        f.ExtraTime();  // Unique to child
    }
}
