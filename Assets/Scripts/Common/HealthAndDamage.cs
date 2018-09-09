using UnityEngine;
using UnityEngine.UI;

namespace ComBlitz.Common
{
    public class HealthAndDamage : MonoBehaviour
    {
        public GameObject deathEffect;
        public float maxHealth;

        [Header("UI Display")]
        public Color minHealthColor = Color.red;

        public Color halfHealthColor = Color.yellow;
        public Color maxHealthColor = Color.green;
        public Slider healthSlider;
        public Image healthFiller;

        private float currentHealth;

        private void Start() => currentHealth = maxHealth;

        private void Update()
        {
            DisplayHealthToUI();
            CheckEnemyDead();
        }

        private void DisplayHealthToUI()
        {
            float maxHealth = this.maxHealth;
            float currentHealthLeft = currentHealth;
            float healthRatio = currentHealthLeft / maxHealth;

            if (healthRatio <= 0.5)
                healthFiller.color = Color.Lerp(minHealthColor, halfHealthColor, healthRatio * 2);
            else
                healthFiller.color = Color.Lerp(halfHealthColor, maxHealthColor, (healthRatio - 0.5f) * 2);
            healthSlider.value = healthRatio;
        }

        private void CheckEnemyDead()
        {
            if (currentHealth <= 0)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            DamageAmountSetter damageAmountSetter = other.GetComponent<DamageAmountSetter>();
            if (damageAmountSetter != null)
                ReduceHealth(damageAmountSetter.damageAmount);
        }

        public void ReduceHealth(float amount) => currentHealth -= amount;
    }
}