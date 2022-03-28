using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Obstacle : Objects
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                GM.LevelLost();
            }
        }
    }
}