using System.Collections.ObjectModel;
using console_invaded;
using console_invaded.Entities;

var width = 40;
var height = 20;
// Console.SetWindowSize(width, height);
// Console.SetBufferSize(width, height);

Console.SetCursorPosition(width / 2 - 5, height / 2);
Console.WriteLine("Console Invaded");
Console.WriteLine("Choose game difficulty:");
Console.WriteLine("0 - Very Easy");
Console.WriteLine("1 - Easy");
Console.WriteLine("2 - Medium");
Console.WriteLine("3 - Hard");
Console.WriteLine("4 - Insane");

var difficulty = (Difficulty)int.Parse(Console.ReadLine()!);
var ruleSet = RuleSetFactory.CreateRuleSet(difficulty);

Console.Clear();

Console.CursorVisible = false;
var gameSpeed = ruleSet.InitGameSpeed;


var player = new Player(width / 2, height - 1, "A", ruleSet);
var invaders = new ObservableCollection<Invader>();
var bullets = new ObservableCollection<Bullet>();
var gameRunning = false;


for (var i = 0; i < ruleSet.InvadersNumber; i++)
{
    var invader = new Invader(i * ruleSet.SpaceBetweenInvaders, 0, "V");

    invader.InvaderHit += () =>
    {
        if (!gameRunning || invaders.Any(i => i.IsActive)) return;
        gameRunning = false;
        Console.Clear();
        Console.SetCursorPosition(width / 2 - 5, height / 2);
        Console.Write("You Win!");
    };
    invaders.Add(invader);
}

gameRunning = true;

var lastInvadersHeight = 0;
while (gameRunning)
{
    var actualInvadersHeight = invaders.Min(i => i.Y * -1);
    if (actualInvadersHeight < lastInvadersHeight)
    {
        if (gameSpeed > ruleSet.MinGameSpeed) gameSpeed -= ruleSet.DecreaseGameSpeedSpan;
    }

    lastInvadersHeight = actualInvadersHeight;

    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey(true).Key;
        switch (key)
        {
            case ConsoleKey.LeftArrow:
                player.MoveLeft();
                break;
            case ConsoleKey.RightArrow:
                player.MoveRight();
                break;
            case ConsoleKey.Spacebar:
                player.Shoot();
                // bullets.Add(new Bullet(player.X, player.Y - 1, "|"));
                break;
            case ConsoleKey.Escape:
                gameRunning = false;
                break;
            default:
                //skip the stroke
                break;
        }
    }

    Console.Clear();
    player.Display();

    foreach (var invader in invaders)
    {
        invader.Display();
        invader.Move();
    }

    foreach (var bullet in player.Bullets)
    {
        bullet.Display();
        bullet.Move();
    }

    // Check for collisions
    foreach (var bullet in player.Bullets)
    {
        if (!gameRunning) break;
        foreach (var invader in invaders)
        {
            if (bullet.IsActive && invader.X == bullet.X && invader.Y == bullet.Y)
            {
                bullet.IsActive = false;
                invader.OnInvaderHit();
            }
        }
    }

    // Remove inactive bullets
    RemoveInactiveBullets(player);

    if (Rules.IsSameHeight(invaders.FirstOrDefault()!, player))
        // if (invaders.Min(x => x.Y) == player.Y)
    {
        gameRunning = false;
        Console.Clear();
        Console.SetCursorPosition(width / 2 - 5, height / 2);
        Console.Write("Game Over");
    }

    Thread.Sleep(gameSpeed);
}

return;

static void RemoveInactiveBullets(Player player)
{
    for (var i = player.Bullets.Count - 1; i >= 0; i--)
    {
        if (!player.Bullets[i].IsActive)
        {
            player.Bullets.RemoveAt(i);
        }
    }
}