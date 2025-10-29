
using System;

public class collectibleData
{
    // Base Class for all stuff to access the player's score whenever
    private static double currentScore = 0;
    private static double scoreMult = 1;

    public double getScore()
    {
        return currentScore;
    }
    public void addScore(double inScore)
    {
        currentScore += inScore * scoreMult;
    }

    public void subtractScore(double inScore)
    {
        currentScore -= inScore;
    }

    public double getScoreMult()
    {
        return scoreMult;
    }

    // use the value of the Meter to determine the multiplier. (0.5 - 1.5 based on -100 to 100)
    public void changeScoreMult(double c)
    {
        // Calculates the influence of the Glumbometer using its stored values (-100 to 100)
        scoreMult += c / 200;
        // Checks in case it goes over/under a certain threshold (to prevent the meter breaking due to having a 0 mult)
        if(scoreMult < 0.5)
        {
            scoreMult = 0.5;
        }    
        if(scoreMult > 1.5)
        {
            scoreMult = 1.5;
        }
    }

    public void resetScore()
    {
        currentScore = 0;
    }

    public string formatScore()
    {
        // Absolute value used to keep negative score from breaking the display
        if (Math.Abs(currentScore) % 1 == 0)
        {
            return "$" + currentScore.ToString() + ".00";
        }
        else if(((Math.Abs(currentScore) % 1) * 100) % 10 == 0)
        {
            return "$" + currentScore.ToString() + "0";
        }
        else
        {
            if (Math.Abs(currentScore) > 1000)
            {
                return "$" + currentScore.ToString().Substring(0, 7);
            }
            else if (Math.Abs(currentScore) > 100)
            {
                return "$" + currentScore.ToString().Substring(0, 6);
            }
            else
            {
                if (currentScore.ToString().Length > 5)
                {
                    return "$" + currentScore.ToString().Substring(0, 5);
                }
                return "$" + currentScore.ToString();
            }
        }
    }
}
