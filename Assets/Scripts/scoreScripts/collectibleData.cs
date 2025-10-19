
public class collectibleData
{
    // Base Class for all stuff to access the player's score whenever
    private static double currentScore = 0;
    private double scoreMult = 1;
    private double glumbometerValue;

    public double getScore()
    {
        return currentScore;
    }
    public void addScore(int inScore)
    {
        currentScore += inScore;
    }

    public void subtractScore(int inScore)
    {
        currentScore -= inScore;
    }

    public double getScoreMult()
    {
        return scoreMult;
    }

    public void changeScoreMult(double c)
    {
        scoreMult += c;
        if (scoreMult < 0)
        {
            scoreMult = 0;
        }
    }

    public void setMeterValue(double g)
    {
        glumbometerValue = g;
    }

    public double getMeterValue()
    {
        return glumbometerValue;
    }

    public void resetScore()
    {
        currentScore = 0;
    }
}
