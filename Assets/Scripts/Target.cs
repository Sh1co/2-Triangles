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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            GM.GainPoint();
            Destroy(gameObject);
        }
    }
}