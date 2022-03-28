using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public Action ScoreUpdated;
    
    
    public int Score { get; private set; } = 0;
    public int HighScore { get; private set; } = 0;

    private void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void Add(int value)
    {
        Score += value;
        if (Score >= HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
        ScoreUpdated?.Invoke();
    }

    public void ResetScore()
    {
        Score = 0;
        ScoreUpdated?.Invoke();
    }
}