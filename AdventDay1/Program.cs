namespace AdventDay1;

class Program
{
    private static void Main(string[] args)
    {
        var reindeers = GetReindeers();
        Part1(reindeers);
        Part2(reindeers);
        Console.ReadLine();
    }

    private static void Part1(IEnumerable<Reindeer> reindeers)
    {
        var most = reindeers.MaxBy(x => x.Calories)!;
        Console.WriteLine($"Reindeer #{most.Index} has {most.Calories} calories");
    }

    private static void Part2(IEnumerable<Reindeer> reindeers)
    {
        var ordered = reindeers.OrderByDescending(x => x.Calories).Take(3);
        Console.WriteLine($"The top 3 reindeer have {ordered.Sum(x => x.Calories)}");
    }

    private static List<Reindeer> GetReindeers()
    {
        using var streamReader = new StreamReader(new FileStream("input.txt", FileMode.Open, FileAccess.Read));
        var allReindeer = new List<Reindeer>();
        int count = 0, index = 1;

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            if (int.TryParse(line, out var current))
            {
                count += current;
            }
            else
            {
                allReindeer.Add(new Reindeer(index++, count));
                count = 0;
            }
        }
        return allReindeer;
    }
}

internal class Reindeer
{
    public int Index { get; }
    public int Calories { get; set; }

    public Reindeer(int index, int calories)
    {
        this.Index = index;
        this.Calories = calories;
    }
}