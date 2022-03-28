using UnityEngine;

namespace DefaultNamespace
{
    public class Objects : MonoBehaviour
    {
        public GameManager GM;
        public float MovementSpeed;
        public float DespawnHeight;
        private void Update()
        {
            transform.Translate(transform.up * -1 * MovementSpeed * Time.deltaTime);
            if (transform.position.y <= DespawnHeight)
            {
                Despawn();
            }
        }

        protected virtual void Despawn()
        {
            Destroy(gameObject);
        }
    }
}