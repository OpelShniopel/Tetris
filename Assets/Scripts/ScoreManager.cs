using UnityEngine;

// TODO: Complete this class to manage the score
public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }
    public int LinesCleared { get; private set; }
    
    public void AddScore(int linesCleared)
    {
        LinesCleared += linesCleared;

        int points = linesCleared switch
        {
            1 => 40,
            2 => 100,
            3 => 300,
            4 => 1200,
            _ => 0
        };

        Score += points;
    }
}
