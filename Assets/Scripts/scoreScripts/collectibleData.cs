
public class collectibleData
{
    private static double currentScore = 0;
    private double scoreMult = 1;
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

}
