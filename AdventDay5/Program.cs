using System.Text.RegularExpressions;

namespace AdventDay5;

internal class Program
{
    private static void Main()
    {
        var inputFile = GetInputFile();

        var stacks = GetStacks(inputFile);
        var stacksCopy = stacks.Select(stack => (CargoStack)stack.Clone()).ToList();
        var moveInstructions = GetMoveInstructions(inputFile);

        CrateMover9000(stacks, moveInstructions);
        CrateMover9001(stacksCopy, moveInstructions);

        Console.Write("Top characters of each stack: ");
        foreach (var stack in stacks) Console.Write(stack.Stack[0]);

        Console.WriteLine();
        Console.Write("Top of original: ");
        foreach (var stack in stacksCopy) Console.Write(stack.Stack[0]);
    }

    private static string GetInputFile()
    {
        return new StreamReader("input.txt").ReadToEnd();
    }

    private static List<CargoStack> GetStacks(string input)
    {
        var lines = input.Split("\r\n")[..8];
        var stackCount = (int)Math.Round((double)lines[0].Length / 4);
        var stacks = new CargoStack[stackCount];

        foreach (var line in lines)
        {
            for (var i = 0; i < stackCount; i++)
            {
                var subString = line.Substring(i * 4, 3);
                var regex = new Regex(@"\[(.*?)\]").Match(subString);

                if (!regex.Success)
                    continue;

                var stackChar = regex.Value[1];

                if (stacks[i] != null)
                    stacks[i].Stack.Add(stackChar);
                else
                    stacks[i] = new CargoStack(stackChar);
            }
        }
        return stacks.ToList();
    }

    private static List<MoveInstruction> GetMoveInstructions(string inputFile)
    {
        var inputLines = inputFile.Split("\r\n")[10..];
        return inputLines.Select(s => new MoveInstruction(s)).ToList();
    }

    private static void CrateMover9000(List<CargoStack> stacks, List<MoveInstruction> moves)
    {
        foreach (var move in moves)
        {
            for (var i = 0; i < move.Crates; i++)
            {
                stacks[move.To].Stack.Insert(0, stacks[move.From].Stack[0]);
                stacks[move.From].Stack.RemoveAt(0);
            }
        }
    }

    private static void CrateMover9001(List<CargoStack> stacks, List<MoveInstruction> moves)
    {
        foreach (var move in moves)
        {
            stacks[move.To].Stack.InsertRange(0, stacks[move.From].Stack.GetRange(0, move.Crates));
            stacks[move.From].Stack.RemoveRange(0, move.Crates);
        }
    }
}