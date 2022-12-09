namespace AdventDay3;

internal class Program
{
    private static List<Rucksack> _input = null!;
    static void Main(string[] args)
    {
        LoadInput();
        Part1();
        Part2();
    }

    private static void LoadInput()
    {
        using var streamReader = new StreamReader("input.txt");
        var result = new List<Rucksack>();

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine()!;
            result.Add(new Rucksack(line));
        }

        _input = result;
    }

    private static void Part1()
    {
        Console.WriteLine($"Sum for part 1: {_input.Sum(SumOfPrioritiesDuplicates)}");
    }

    private static void Part2()
    {
        var priorities = 0;

        for (var i = 0; i < _input.Count; i+=3)
        {
            var ruck1 = _input[i];
            var ruck2 = _input[i+1];
            var ruck3 = _input[i+2];

            var common = ruck1.AllItems.Intersect(ruck2.AllItems).Intersect(ruck3.AllItems);
            priorities += common.Sum(GetPriority);
        }
        Console.WriteLine($"Sum for part 2: {priorities}");
    }

    private static int SumOfPrioritiesDuplicates(Rucksack ruck)
    {
        var firstDistinct = ruck.FirstCompartment.Distinct();
        var secondDistinct = ruck.SecondCompartment.Distinct();
        return firstDistinct.Where(c => secondDistinct.Contains(c)).Sum(GetPriority);
    }

    private static int GetPriority(char value)
    {
        return char.IsLower(value) ? value - 96 : value - 38;
    }
}

internal class Rucksack
{
    public string AllItems { get; set; }
    public string FirstCompartment { get; set; }
    public string SecondCompartment { get; set; }
    public Rucksack(string items)
    {
        var size = items.Length / 2;
        FirstCompartment = items[..size];
        SecondCompartment = items[size..];
        AllItems = items;
    }
}