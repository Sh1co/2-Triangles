using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public Action<int> ScoreUpdated;

    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void Add(int value)
    {
        _score += value;
        if (_score <= _highScore) return;
        _highScore = _score;
        PlayerPrefs.SetInt("HighScore", _highScore);
    }

    public void ResetScore()
    {
        _score = 0;
    }

    private int _score = 0;
    private int _highScore = 0;
}