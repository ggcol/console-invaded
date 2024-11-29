namespace console_invaded.Entities;

internal sealed class Player(int x, int y, string symbol, RuleSet ruleSet)
    : ScreenEntity(x, y, symbol)
{
    public void MoveLeft()
    {
        if (X > 0) X--;
    }

    public void MoveRight()
    {
        if (X < Console.WindowWidth - 1) X++;
    }
    
    public List<Bullet> Bullets { get; } = new();
    
    public void Shoot()
    {
        if (Bullets.Count >= ruleSet.MaxConcurrentBullets) return;
        
        Bullets.Add(new Bullet(X, Y - 1, "*"));
    }
}