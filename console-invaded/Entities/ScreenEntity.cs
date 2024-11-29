namespace console_invaded.Entities;

public abstract class ScreenEntity(int x, int y, string symbol)
{
    private string _symbol = symbol;
    private int _y = y;
    private int _x = x;

    public int X
    {
        get => _x;
        protected set => _x = value;
    }

    public int Y
    {
        get => _y;
        protected set => _y = value;
    }
    
    public string Symbol
    {
        get => _symbol;
        set => _symbol = value;
    }

    public void Display()
    {
        Console.SetCursorPosition(_x, _y);
        Console.Write(_symbol);
    }
}