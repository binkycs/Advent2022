using System.Diagnostics;

namespace AdventDay10;

internal class Program
{
    private static void Main()
    {
        var input = new StreamReader("input.txt").ReadToEnd().Split("\r\n");
        var interpreter = new Interpreter();

        interpreter.Run(input);
    }
}

internal class Interpreter
{
    private Queue<(int Value, int FinishCycle)> AddQueue { get; }
    private List<int> SignalStrengths { get; }
    private int X { get; set; }
    public int Cycles { get; set; }
    public (int X, int Y) CrtPosition;

    private char[][] CrtOutput { get; }

    public Interpreter()
    {
        this.AddQueue = new Queue<(int, int)>();
        this.SignalStrengths = new List<int>();
        this.X = 1;
        CrtOutput = new char[6][]
        {
            new char[40],
            new char[40],
            new char[40],
            new char[40],
            new char[40],
            new char[40],
        };
    }

    public void Run(string[] input)
    {
        foreach (var line in input)
        {
            if (line == "noop")
            {
                Cycles++;
            }
            else
            {
                Cycles+=2;
                var numToAdd = int.Parse(line.Split(' ')[1]);
                AddQueue.Enqueue((numToAdd, Cycles));
            }
        }

        ProcessQueue();
        Console.WriteLine($"Sum of Signal Strengths: {SignalStrengths.Sum()}\n");

        foreach (var outputLine in CrtOutput)
            Console.WriteLine(outputLine);
    }

    private void ProcessQueue()
    {
        for (var i = 0; i < Cycles; i++)
        {
            if (i is 20 or 60 or 100 or 140 or 180 or 220)
                SignalStrengths.Add(this.X * i);

            if (AddQueue.Count > 0 && AddQueue.Peek().FinishCycle == i)
                this.X += AddQueue.Dequeue().Value;

            ProcessCrt();
        }
    }

    private void ProcessCrt()
    {
        if (this.X - 1 == CrtPosition.X)
            CrtOutput[CrtPosition.Y][CrtPosition.X] = '#';
        else if (this.X == CrtPosition.X)
            CrtOutput[CrtPosition.Y][CrtPosition.X] = '#';
        else if (this.X + 1 == CrtPosition.X)
            CrtOutput[CrtPosition.Y][CrtPosition.X] = '#';
        else
            CrtOutput[CrtPosition.Y][CrtPosition.X] = '.';

        if (CrtPosition.X == 39)
        {
            CrtPosition.X = 0;
            CrtPosition.Y++;
        }
        else
        {
            CrtPosition.X++;
        }
    }
}