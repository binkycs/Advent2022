using System.Diagnostics;

namespace AdventDay8;

internal class Program
{
    public const int MaxRows = 99;
    public const int MaxColumns = 99;
    private static readonly Tree[,] TreeList = new Tree[MaxRows,MaxColumns];

    private static void Main()
    {
        BuildTreeList();
        Part1();
        Part2();
    }

    private static void BuildTreeList()
    {
        var input = new StreamReader("input.txt").ReadToEnd().Split("\r\n");
        var row = 0;
        var col = 0;
        foreach (var inputRow in input)
        {
            foreach (var c in inputRow)
            {
                TreeList[row, col] = new Tree(row, col, int.Parse(c.ToString()));
                col++;
            }
            col = 0;
            row++;
        }
    }

    private static void Part1()
    {
        var visibleTrees = TreeList.Cast<Tree>().Count(IsVisible);

        Console.WriteLine("Visible trees: " + visibleTrees);
    }

    private static void Part2()
    {
        Tuple<Tree, int>? bestView = null;
        foreach (var tree in TreeList)
        {
            var x = GetScenicScore(tree);
            if (bestView == null || x > bestView.Item2)
                bestView = new Tuple<Tree, int>(tree, x);
        }

        Console.WriteLine($"Best scenic score:\n\tCoordinates: [ {bestView!.Item1.Row}, {bestView.Item1.Column} ]\n\tScore: {bestView.Item2}");
    }

    public static int GetScenicScore(Tree tree)
    {
        return GetScenicScoreLeft(tree)
               * GetScenicScoreRight( tree)
               * GetScenicScoreUp(tree)
               * GetScenicScoreDown(tree);
    }

    private static int GetScenicScoreRight(Tree tree)
    {
        var treesVisible = 0;
        for (var column = tree.Column + 1; column < MaxColumns; column++)
            if (TreeList[tree.Row, column].Size < tree.Size)
                treesVisible++;
            else
                return treesVisible + 1;

        return treesVisible;
    }

    private static int GetScenicScoreLeft(Tree tree)
    {
        var treesVisible = 0;
        for (var column = tree.Column - 1; column > -1; column--)
            if (TreeList[tree.Row, column].Size < tree.Size)
                treesVisible++;
            else
                return treesVisible + 1;

        return treesVisible;
    }

    private static int GetScenicScoreUp(Tree tree)
    {
        var treesVisible = 0;
        for (var row = tree.Row - 1; row > -1; row--)
            if (TreeList[row, tree.Column].Size < tree.Size)
                treesVisible++;
            else
                return treesVisible + 1;

        return treesVisible;
    }

    private static int GetScenicScoreDown(Tree tree)
    {
        var treesVisible = 0;
        for (var row = tree.Row + 1; row < MaxRows; row++)
            if (TreeList[row, tree.Column].Size < tree.Size)
                treesVisible++;
            else
                return treesVisible + 1;

        return treesVisible;
    }

    private static bool IsVisible(Tree tree)
    {
        return IsVisibleUp(tree) || IsVisibleToRight(tree) || IsVisibleToLeft(tree) ||  IsVisibleDown(tree);
    }

    private static bool IsVisibleToRight(Tree tree)
    {
        for (var column = tree.Column + 1; column < MaxColumns; column++)
            if (TreeList[tree.Row, column].Size >= tree.Size)
                return false;

        return true;
    }
    private static bool IsVisibleToLeft(Tree tree)
    {
        for (var column = tree.Column - 1; column > -1; column--)
            if (TreeList[tree.Row, column].Size >= tree.Size)
                return false;

        return true;
    }
    private static bool IsVisibleUp(Tree tree)
    {
        for (var row = tree.Row - 1; row > -1; row--)
            if (TreeList[row, tree.Column].Size >= tree.Size)
                return false;

        return true;
    }
    private static bool IsVisibleDown(Tree tree)
    {
        for (var row = tree.Row + 1; row < MaxRows; row++)
            if (TreeList[row, tree.Column].Size >= tree.Size)
                return false;

        return true;
    }
}

internal class Tree
{
    public int Row { get; }
    public int Column { get; }
    public int Size { get; }
    public Tree(int row, int column, int size)
    {
        this.Row = row;
        this.Column = column;
        this.Size = size;
    }
}