
public class collectibleData
{
    private static double currentScore = 0;
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
}
