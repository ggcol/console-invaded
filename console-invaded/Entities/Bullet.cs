namespace console_invaded.Entities;

class Bullet(int x, int y, string symbol) 
: ScreenEntity(x, y, symbol)
{
    public bool IsActive { get; set; } = true;

    public void Move()
    {
        if (!IsActive) return;
        
        Y--;
        if (Y < 0)
        {
            IsActive = false;
        }
    }
}