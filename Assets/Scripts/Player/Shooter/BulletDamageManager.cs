using UnityEngine;

namespace DeBomb.Player.Shooter
{
    class BulletDamageManager : MonoBehaviour
    {
        public float bulletDamage = 20f;
        
        private void OnTriggerEnter(Collider other) => Destroy(gameObject);
    }
}
