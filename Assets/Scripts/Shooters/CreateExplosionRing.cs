using ComBlitz.Common;
using UnityEngine;

namespace ComBlitz.Shooters
{
    [RequireComponent(typeof(DamageAmountSetter))]
    public class CreateExplosionRing : MonoBehaviour
    {
        public GameObject explosionEffect;
        public float explosionRadius;

        private DamageAmountSetter damageAmountSetter;

        private void Start() => damageAmountSetter = GetComponent<DamageAmountSetter>();

        private void OnTriggerEnter(Collider other)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                HealthAndDamage healthAndDamage = collider.GetComponent<HealthAndDamage>();
                if (healthAndDamage != null)
                    healthAndDamage.ReduceHealth(damageAmountSetter.damageAmount);
            }

            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}