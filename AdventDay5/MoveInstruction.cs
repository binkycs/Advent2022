namespace AdventDay5;

internal class MoveInstruction
{
    public int Crates { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public MoveInstruction(string input)
    {
        var words = input.Split(' ');
        var numbers = new List<int>();
        foreach (var word in words)
        {
            if (int.TryParse(word, out var number))
                numbers.Add(number);
        }
        this.Crates = numbers[0];
        this.From = numbers[1] - 1;
        this.To = numbers[2] - 1;
    }
}