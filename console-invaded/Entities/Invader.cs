namespace console_invaded.Entities;

internal sealed class Invader(int x, int y, string symbol)
    : ScreenEntity(x, y, symbol)
{
    private bool _movingRight = true;
    public bool IsActive { get; set; } = true;

    public event Action? InvaderHit;

    public void Move()
    {
        if (_movingRight)
        {
            X++;
            if (X < Console.WindowWidth - 1) return;
            _movingRight = false;
        }
        else
        {
            X--;
            if (X > 0) return;
            _movingRight = true;
        }

        Y++;
    }

    public void OnInvaderHit()
    {
        IsActive = false;
        Symbol = string.Empty;
        InvaderHit?.Invoke();
    }
}