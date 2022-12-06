namespace AdventDay5;

internal class CargoStack : ICloneable
{
    public List<char> Stack { get; set; }

    public CargoStack()
    {
        this.Stack = new List<char>();
    }

    public CargoStack(char input)
    {
        this.Stack = new List<char> { input };
    }

    public object Clone()
    {
        var stackClone = new CargoStack();
        stackClone.Stack.AddRange(this.Stack);
        return stackClone;
    }
}