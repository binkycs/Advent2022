namespace AdventDay4;

internal class Program
{
    private static List<SectionCleanup> _input = null!;

    private static void Main()
    {
        LoadInput();
        Part1();
        Part2();
    }

    private static void Part1()
    {
        var result = FindFullyContainedSections(_input);
        Console.WriteLine($"Fully contained assignments: {result}");
    }

    private static void Part2()
    {
        var result = FindOverlappedSections(_input);
        Console.WriteLine($"Overlapped assignments: {result}");
    }

    private static int FindFullyContainedSections(IEnumerable<SectionCleanup> input)
    {
        var result = 0;
        foreach (var section in input)
        {
            if (section.Section2.First() >= section.Section1.First() && section.Section2.Last() <= section.Section1.Last())
            {
                result++;
                continue;
            }

            if (section.Section1.First() >= section.Section2.First() && section.Section1.Last() <= section.Section2.Last())
                result++;
        }

        return result;
    }

    private static int FindOverlappedSections(IEnumerable<SectionCleanup> input)
    {
        var result = 0;
        foreach (var section in input)
        {
            if (section.Section1.Intersect(section.Section2).Any())
            {
                result++;
                continue;
            }

            if (section.Section2.Intersect(section.Section1).Any())
                result++;
        }

        return result;
    }

    private static void LoadInput()
    {
        using var streamReader = new StreamReader(new FileStream("input.txt", FileMode.Open, FileAccess.Read));
        var result = new List<SectionCleanup>();

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine()!;
            result.Add(new SectionCleanup(line));
        }

        _input = result;
    }
}

internal class SectionCleanup
{
    public int[] Section1 { get; set; }
    public int[] Section2 { get; set; }

    public SectionCleanup(string input)
    {
        var sections = input.Split(',');

        this.Section1 = ParseRange(sections[0]);
        this.Section2 = ParseRange(sections[1]);
    }

    private static int[] ParseRange(string input)
    {
        var parts = input.Split('-');

        var first = int.Parse(parts[0]);
        var second = int.Parse(parts[1]);

        return Enumerable.Range(first, second - first + 1).ToArray();
    }
}