namespace AdventDay2;

internal class Program
{
    private static void Main(string[] args)
    {
        var games = GetGames();
        Part1(games);
        Part2(games);
        Console.ReadLine();
    }

    private static void Part1(IEnumerable<Game> games)
    {
        var sum = games.Sum(x => x.CalculateScore(true));
        Console.WriteLine($"Total score from list of games: {sum}");
    }

    private static void Part2(IEnumerable<Game> games)
    {
        var sum = games.Sum(x => x.CalculateScore(false));
        Console.WriteLine($"Total score from list of games: {sum}");
    }

    private static List<Game> GetGames()
    {
        using var streamReader = new StreamReader("input.txt");
        var result = new List<Game>();

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine()!;
            var values = line.Split(' ');
            var enemyChoice = values[0][0];
            var ownChoice = values[1][0];
            result.Add(new Game(enemyChoice, ownChoice));
        }
        return result;
    }
}

internal class Game
{
    private RockPaperScissors EnemyChoice { get; }
    private char OwnChar { get; }

    private readonly Dictionary<(RockPaperScissors, RockPaperScissors), int> _scoreTable =
        new()
        {
            [(RockPaperScissors.Rock, RockPaperScissors.Rock)] = 3,
            [(RockPaperScissors.Rock, RockPaperScissors.Paper)] = 0,
            [(RockPaperScissors.Rock, RockPaperScissors.Scissors)] = 6,
            [(RockPaperScissors.Paper, RockPaperScissors.Rock)] = 6,
            [(RockPaperScissors.Paper, RockPaperScissors.Paper)] = 3,
            [(RockPaperScissors.Paper, RockPaperScissors.Scissors)] = 0,
            [(RockPaperScissors.Scissors, RockPaperScissors.Rock)] = 0,
            [(RockPaperScissors.Scissors, RockPaperScissors.Paper)] = 6,
            [(RockPaperScissors.Scissors, RockPaperScissors.Scissors)] = 3
        };

    public Game(char enemyChar, char ownChar)
    {
        this.EnemyChoice = GetEnemyChoice(enemyChar);
        this.OwnChar = ownChar;
    }

    public int CalculateScore(bool partOne)
    {
        var ownChoice = GetOwnChoice(partOne);
        var score = _scoreTable[(ownChoice, this.EnemyChoice)] + ownChoice switch
        {
            RockPaperScissors.Rock => 1,
            RockPaperScissors.Paper => 2,
            RockPaperScissors.Scissors => 3,
            _ => throw new ArgumentOutOfRangeException()
        };

        return score;
    }

    private RockPaperScissors GetEnemyChoice(char value)
    {
        return value switch
        {
            'A' => RockPaperScissors.Rock,
            'B' => RockPaperScissors.Paper,
            'C' => RockPaperScissors.Scissors,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private RockPaperScissors GetOwnChoice(bool partOne)
    {
        if (partOne)
        {
            return OwnChar switch
            {
                'X' => RockPaperScissors.Rock,
                'Y' => RockPaperScissors.Paper,
                'Z' => RockPaperScissors.Scissors,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        return OwnChar switch
        {
            'X' => GetOwnChoiceFromWinning(false),
            'Y' => this.EnemyChoice,
            'Z' => GetOwnChoiceFromWinning(true),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public RockPaperScissors GetOwnChoiceFromWinning(bool winning)
    {
        return this.EnemyChoice switch
        {
            RockPaperScissors.Rock => winning ? RockPaperScissors.Paper : RockPaperScissors.Scissors,
            RockPaperScissors.Paper => winning ? RockPaperScissors.Scissors : RockPaperScissors.Rock,
            RockPaperScissors.Scissors => winning ? RockPaperScissors.Rock : RockPaperScissors.Paper,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

enum RockPaperScissors
{
    Rock,
    Paper,
    Scissors
}