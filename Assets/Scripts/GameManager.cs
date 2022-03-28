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


    public void LevelLost()
    {
        
    }
    
    public void GainPoint()
    {
        
    }
    
    
    private void Start()
    {
        spawnHeight = Screen.height * 1.1f;
        spawnHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, spawnHeight, 0)).y;
        
        despawnHeight = -Screen.height * 0.1f;
        despawnHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, spawnHeight, 0)).y;
    }

    private void Update()
    {
        IncrementCounters(Time.deltaTime);
        if (spawnCounter1 >= _initialSpawnDelay)
        {
            spawnCounter1 = 0;
            SpawnObject(true);
        }
        if (spawnCounter2 >= _initialSpawnDelay)
        {
            spawnCounter2 = 0;
            SpawnObject(true);
        }
        
    }

    private void IncrementCounters(float deltaTime)
    {
        spawnCounter1 += deltaTime;
        spawnCounter2 += deltaTime;
    }

    private void SpawnObject(bool car1)
    {
        var obstacle = car1 ? _obstacle1 : _obstacle2;
        var target = car1 ? _target1 : _target2;
        
        var leftLane = Screen.width * (car1? 0.125f : 0.625f);
        var rightLane = Screen.width * (car1? 0.375f : 0.875f);
        
        leftLane = Camera.main.ScreenToWorldPoint(new Vector3(leftLane, 0, 0)).x;
        rightLane = Camera.main.ScreenToWorldPoint(new Vector3(rightLane, 0, 0)).x;

        var objectOdds = new Random().Next(100);
        var laneOdds = new Random().Next(100);

        var spawnObj = objectOdds >= 50 ? obstacle : target;
        var spawnLane = laneOdds >= 50 ? rightLane : leftLane;

        var obj = Instantiate(spawnObj, new Vector3(spawnLane, spawnHeight), quaternion.identity);
        obj.GM = this;
        obj.MovementSpeed = _movementSpeed;
        obj.DespawnHeight = despawnHeight;
    }
    

    private float spawnCounter1 = 0;
    private float spawnCounter2 = -0.1f;
    private float spawnHeight;
    private float despawnHeight;
}
