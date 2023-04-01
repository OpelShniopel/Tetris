using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int Score { get; set; }
    private int LinesCleared { get; set; }
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI linesClearedText;
    
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
        
        UpdateScoreText();
        UpdateLinesClearedText();
    }
    
    private void UpdateScoreText()
    {
        if (scoreText)
        {
            scoreText.text = $"Score: {Score}";
        }
    }
    
    private void UpdateLinesClearedText()
    {
        if (linesClearedText)
        {
            linesClearedText.text = $"Lines Cleared: {LinesCleared}";
        }
    }
    
    public void ResetScore()
    {
        Score = 0;
        LinesCleared = 0;
    }
}
