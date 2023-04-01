using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; set; }
    public int LinesCleared { get; set; }
    public int Level { get; set; }
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI linesClearedText;
    [SerializeField] private TextMeshProUGUI levelText;
    
    public void AddScore(int linesCleared)
    {
        LinesCleared += linesCleared;
        Level = LinesCleared / 10;

        // For level system (kitam sprintui)
        // int points = linesCleared switch
        // {
        //     1 => 40 * (Level + 1),
        //     2 => 100 * (Level + 1),
        //     3 => 300 * (Level + 1),
        //     4 => 1200 * (Level + 1),
        //     _ => 0
        // };
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
        // UpdateLinesClearedText();
        UpdateLevelText();
    }
    
    private void UpdateScoreText()
    {
        if (scoreText)
        {
            scoreText.text = $"Score: {Score}";
        }
    }
    
    // Kitam sprintui
    // private void UpdateLinesClearedText()
    // {
    //     if (linesClearedText)
    //     {
    //         linesClearedText.text = $"Lines Cleared: {LinesCleared}";
    //     }
    // }
    
    private void UpdateLevelText()
    {
        if (levelText)
        {
            levelText.text = $"Level: {Level}";
        }
    }
    
    public float GetUpdatedStepDelay()
    {
        float speedIncrease = 0.10f * LinesCleared;
        float minStepDelay = 0.05f;
        return Mathf.Max(1f - speedIncrease, minStepDelay);
    }
    
    // Speed increases every 10 lines cleared (kitam sprintui)
    // public float GetUpdatedStepDelay()
    // {
    //     int level = LinesCleared / 10; // Change level every 10 lines cleared
    //     float speedIncreasePerLevel = 0.10f;
    //     float speedIncrease = speedIncreasePerLevel * level;
    //     float minStepDelay = 0.05f;
    //
    //     return Mathf.Max(1f - speedIncrease, minStepDelay);
    // }
    
    public void ResetScore()
    {
        Score = 0;
        LinesCleared = 0;
    }
}
