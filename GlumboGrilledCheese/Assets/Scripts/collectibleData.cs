using UnityEngine;

public class collectibleData
{
    private static int currentScore = 0;
    public int getScore()
    {
        return currentScore;
    }
    public void addScore(int inScore)
    {
        currentScore = currentScore + inScore;
    }

}
