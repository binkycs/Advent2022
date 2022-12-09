namespace AdventDay6;

internal class Program
{
    private static void Main()
    {
        var input = GetInput();
        CommunicationChecker9001(input, 4);
        CommunicationChecker9001(input, 14);
    }

    private static string GetInput()
    {
        return new StreamReader("input.txt").ReadToEnd();
    }

    private static void CommunicationChecker9001(string input, int uniqueCharacters)
    {
        var characters = string.Empty;

        for (var i = 0; i < input.Length; i++)
        {
            if (characters.Length == uniqueCharacters)
                characters = characters.Remove(0, 1);

            characters += input[i];

            if (characters.Length == uniqueCharacters && !ContainsDuplicates(characters))
            {
                Console.WriteLine($"First marker after: {i + 1}");
                break;
            }
        }
    }

    private static bool ContainsDuplicates(string input)
    {
        return input.GroupBy(c => c).Any(g => g.Count() > 1);
    }
}