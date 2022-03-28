using System;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Objects _obstacle1;
    [SerializeField] private Objects _obstacle2;
    [SerializeField] private Objects _target1;
    [SerializeField] private Objects _target2;
    [SerializeField] private float _initialSpawnDelay = 1.0f;
    [SerializeField] private float _movementSpeed = 5.0f;
    [SerializeField] private GameObject _levelLostScreen;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameplayHUD;
    [SerializeField] private GameObject _startMenu;


    public bool IsPlaying()
    {
        return playing;
    }
    
    public void LevelLost()
    {
        Pause();
        _levelLostScreen.SetActive(true);
        _gameplayHUD.SetActive(false);
    }

    public void ResetLevel()
    {
        _levelLostScreen.SetActive(false);
        _scoreCounter.ResetScore();
        _gameplayHUD.SetActive(true);
        foreach (var obj in GameObject.FindGameObjectsWithTag("Object"))
        {
            Destroy(obj);
        }
        playing = true;
    }
    
    public void GainPoint()
    {
        _scoreCounter.Add(1);
    }

    public void PauseLevel()
    {
        Pause();
        _pausePanel.SetActive(true);
        _gameplayHUD.SetActive(false);
    }

    public void Play()
    {
        _pausePanel.SetActive(false);
        _startMenu.SetActive(false);
        _gameplayHUD.SetActive(true);
        foreach (var obj in GameObject.FindGameObjectsWithTag("Object"))
        {
            obj.GetComponent<Objects>().MovementSpeed = _movementSpeed;
        }

        playing = true;
    }

    private void Pause()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Object"))
        {
            obj.GetComponent<Objects>().MovementSpeed = 0;
        }

        playing = false;
    }
    
    private void Start()
    {
        _spawnHeight = Screen.height * 1.1f;
        _spawnHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, _spawnHeight, 0)).y;
        
        _despawnHeight = -Screen.height * 0.1f;
        _despawnHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, _spawnHeight, 0)).y;

        _scoreCounter = GetComponent<ScoreCounter>();

        rnd1 = new Random(83259857);
        rnd2 = new Random(27508295);
    }

    private void Update()
    {
        if (!playing) return;
        IncrementCounters(Time.deltaTime);
        if (_spawnCounter1 >= _initialSpawnDelay)
        {
            _spawnCounter1 = 0;
            SpawnObject(true);
        }
        if (_spawnCounter2 >= _initialSpawnDelay)
        {
            _spawnCounter2 = 0;
            SpawnObject(false);
        }
        
    }

    private void IncrementCounters(float deltaTime)
    {
        _spawnCounter1 += deltaTime;
        _spawnCounter2 += deltaTime;
    }

    private void SpawnObject(bool car1)
    {
        var obstacle = car1 ? _obstacle1 : _obstacle2;
        var target = car1 ? _target1 : _target2;
        
        var leftLane = Screen.width * (car1? 0.125f : 0.625f);
        var rightLane = Screen.width * (car1? 0.375f : 0.875f);
        
        leftLane = Camera.main.ScreenToWorldPoint(new Vector3(leftLane, 0, 0)).x;
        rightLane = Camera.main.ScreenToWorldPoint(new Vector3(rightLane, 0, 0)).x;

        var objectOdds = rnd1.Next(100);
        var laneOdds = rnd2.Next(100);
        
        Debug.Log(objectOdds + " " + laneOdds);

        var spawnObj = objectOdds >= 50 ? obstacle : target;
        var spawnLane = laneOdds >= 50 ? rightLane : leftLane;

        var obj = Instantiate(spawnObj, new Vector3(spawnLane, _spawnHeight), quaternion.identity);
        obj.GM = this;
        obj.MovementSpeed = _movementSpeed;
        obj.DespawnHeight = _despawnHeight;
    }
    

    private float _spawnCounter1 = 0;
    private float _spawnCounter2 = -0.1f;
    private float _spawnHeight;
    private float _despawnHeight;
    private ScoreCounter _scoreCounter;
    private bool playing;
    private Random rnd1;
    private Random rnd2;
}
