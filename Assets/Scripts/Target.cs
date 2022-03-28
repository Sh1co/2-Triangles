using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Target : Objects
    {
        protected override void Despawn()
        {
            GM.LevelLost();
            base.Despawn();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            GM.GainPoint();
            Destroy(gameObject);
        }
    }
}