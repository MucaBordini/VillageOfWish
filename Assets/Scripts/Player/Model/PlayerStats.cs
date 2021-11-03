public class PlayerStats
{

    private static PlayerStats instance;
    private int healthPoints = 3;
    private int points = 500;

    private PlayerStats()
    {

    }

    public static PlayerStats getIstance()
    {
        if (instance == null)
            instance = new PlayerStats();
        return instance;
    }

    public int getHealthPoints()
    {
        return healthPoints;
    }

    public int getPoints()
    {
        return points;
    }

    public void healthLoss()
    {
        healthPoints--;
    }

    public void healthGain()
    {
         healthPoints++;
    }

    public void healthReset()
    {
        healthPoints = 3;
    }

    public void addPoints()
    {
        points += 50;
    }
    public void bonusPoints()
    {
        points += 100;
    }
    public void usePoints()
    {
        points -= 100;
    }

    public void setHealth(int health)
    {
        healthPoints = health;
    }

    public void setPoints(int pointsNew)
    {
        points = pointsNew;
    }
}
