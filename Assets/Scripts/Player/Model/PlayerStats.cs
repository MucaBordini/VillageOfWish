public class PlayerStats
{

    private static PlayerStats instance;
    private int healthPoints = 3;
    private int practicePoints = 0;

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

    public int getPracticePoints()
    {
        return practicePoints;
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

    public void addPracticePoints()
    {
        practicePoints += 100;
    }
}
