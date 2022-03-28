using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameplayScore;
    [SerializeField] private TextMeshProUGUI _endScreenScore;
    [SerializeField] private TextMeshProUGUI _highScoreScore;

    private void Start()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
        _scoreCounter.ScoreUpdated += UpdateScoreUI;

    }

    private void OnDestroy()
    {
        _scoreCounter.ScoreUpdated -= UpdateScoreUI;
    }


    private void UpdateScoreUI()
    {
        _gameplayScore.text = _scoreCounter.Score.ToString();
        _endScreenScore.text = _scoreCounter.Score.ToString();
        _highScoreScore.text = _scoreCounter.HighScore.ToString();
    }
    

    private ScoreCounter _scoreCounter;
}
