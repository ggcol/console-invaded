using console_invaded.Entities;

namespace console_invaded;

public static class Rules
{
    public static bool IsSameHeight(ScreenEntity entity1, ScreenEntity entity2)
    {
        return entity1.Y == entity2.Y;
    }
}

public enum Difficulty
{
    VeryEasy,
    Easy,
    Medium,
    Hard,
    Insane
}


public static class RuleSetFactory
{
    public static RuleSet CreateRuleSet(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.VeryEasy => new VeryEasyRuleSet(),
            Difficulty.Easy => new EasyRuleSet(),
            Difficulty.Medium => new MediumRuleSet(),
            Difficulty.Hard => new HardRuleSet(),
            Difficulty.Insane => new InsaneRuleSet(),
            _ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
        };
    }
}


public abstract class RuleSet
{
    public int InitGameSpeed { get; set; }
    public int DecreaseGameSpeedSpan { get; set; }
    public int MinGameSpeed { get; set; }
    public int InvadersNumber { get; set; }
    public int SpaceBetweenInvaders { get; set; }
    public int MaxConcurrentBullets { get; set; }
}

public class VeryEasyRuleSet : RuleSet
{
    public VeryEasyRuleSet()
    {
        InitGameSpeed = 100;
        DecreaseGameSpeedSpan = 10;
        MinGameSpeed = 30;
        InvadersNumber = 12;
        SpaceBetweenInvaders = 2;
        MaxConcurrentBullets = 30;
    }
}

public class EasyRuleSet : RuleSet
{
    public EasyRuleSet()
    {
        InitGameSpeed = 100;
        DecreaseGameSpeedSpan = 10;
        MinGameSpeed = 20;
        InvadersNumber = 16;
        SpaceBetweenInvaders = 4;
        MaxConcurrentBullets = 20;
    }
}

public class MediumRuleSet : RuleSet
{
    public MediumRuleSet()
    {
        InitGameSpeed = 80;
        DecreaseGameSpeedSpan = 10;
        MinGameSpeed = 20;
        InvadersNumber = 24;
        SpaceBetweenInvaders = 4;
        MaxConcurrentBullets = 20;
    }
}

public class HardRuleSet : RuleSet
{
    public HardRuleSet()
    {
        InitGameSpeed = 60;
        DecreaseGameSpeedSpan = 10;
        MinGameSpeed = 10;
        InvadersNumber = 24;
        SpaceBetweenInvaders = 8;
        MaxConcurrentBullets = 15;
    }
}

public class InsaneRuleSet : RuleSet
{
    public InsaneRuleSet()
    {
        InitGameSpeed = 60;
        DecreaseGameSpeedSpan = 20;
        MinGameSpeed = 0;
        InvadersNumber = 24;
        SpaceBetweenInvaders = 12;
        MaxConcurrentBullets = 15; 
    }
}

