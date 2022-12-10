namespace AdventDay9;

internal class Program
{
    private static readonly Dictionary<string, int> Directions = new()
    {
        {"U", -1},
        {"D", 1},
        {"L", -1},
        {"R", 1}
    };

    private static void Main()
    {
        var input = new StreamReader("input.txt").ReadToEnd().Split("\r\n");

        var rope1 = new Rope(2);
        var rope2 = new Rope(10);
        foreach (var line in input)
        {
            var command = line.Split(' ');
            var direction = Directions.First(x => x.Key == command[0]);
            var length = int.Parse(command[1]);

            rope1.ProcessMove(direction, length);
            rope2.ProcessMove(direction, length);
        }

        Console.WriteLine($"Total rope positions (part 1): {rope1.UniqueTailPositions.Count}");
        Console.WriteLine($"Total rope positions (part 2): {rope2.UniqueTailPositions.Count}");
    }

    private class Rope
    {
        private Knot[] Knots { get; set; }
        public readonly HashSet<(int X, int Y)> UniqueTailPositions = new();

        public Rope(int knots)
        {
            Knots = new Knot[knots];
            for (var i = 0; i < knots; i++)
                Knots[i] = new Knot((0, 0));

            UniqueTailPositions.Add((0, 0));
        }

        public void ProcessMove(KeyValuePair<string, int> direction, int length)
        {
            for (var i = 0; i < length; i++)
            {
                UpdateHeadLocation(direction);
                UpdateTailLocation();
            }
        }

        private void UpdateHeadLocation(KeyValuePair<string, int> moveInstructions)
        {
            if (moveInstructions.Key is "U" or "D")
                Knots[0].Position.Y += moveInstructions.Value;
            else if (moveInstructions.Key is "L" or "R")
                Knots[0].Position.X += moveInstructions.Value;
        }

        private void UpdateTailLocation()
        {
            for (var i = 1; i < Knots.Length; i++)
            {
                if (!IsFar(i))
                    return;
                if (Knots[i - 1].Position.Y > Knots[i].Position.Y)
                    Knots[i].Position.Y++;
                if (Knots[i - 1].Position.Y < Knots[i].Position.Y)
                    Knots[i].Position.Y--;
                if (Knots[i - 1].Position.X > Knots[i].Position.X)
                    Knots[i].Position.X++;
                if (Knots[i - 1].Position.X < Knots[i].Position.X)
                    Knots[i].Position.X--;
            }

            var tail = Knots[^1];
            UniqueTailPositions.Add((tail.Position.X, tail.Position.Y));
        }

        private bool IsFar(int index)
        {
            return Math.Max(Math.Abs(Knots[index - 1].Position.X - Knots[index].Position.X),
                       Math.Abs(Knots[index - 1].Position.Y - Knots[index].Position.Y)) > 1;
        }

        private class Knot
        {
            public (int X, int Y) Position;
            public Knot((int X, int Y) position)
            {
                Position = position;
            }
        }
    }
}