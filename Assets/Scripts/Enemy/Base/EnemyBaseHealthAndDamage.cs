using UnityEngine;
using UnityEngine.UI;

namespace ComBlitz.Enemy.Base
{
    public abstract class EnemyBaseHealthAndDamage : MonoBehaviour
    {
        public GameObject enemyDeathEffect;
        public float maxEnemyHealth;

        [Header("Enemy UI")]
        public Color minHealthColor = Color.red;

        public Color halfHealthColor = Color.yellow;
        public Color maxHealthColor = Color.green;
        public Slider enemyHealthSlider;
        public Image enemyHealthFiller;

        private float currentEnemyHealth;

        private void Start() => currentEnemyHealth = maxEnemyHealth;

        private void Update()
        {
            DisplayHealthToUI();
            CheckEnemyDead();
        }

        private void DisplayHealthToUI()
        {
            float maxHealth = maxEnemyHealth;
            float currentHealthLeft = currentEnemyHealth;
            float healthRatio = currentHealthLeft / maxHealth;

            if (healthRatio <= 0.5)
                enemyHealthFiller.color = Color.Lerp(minHealthColor, halfHealthColor, healthRatio * 2);
            else
                enemyHealthFiller.color = Color.Lerp(halfHealthColor, maxHealthColor, (healthRatio - 0.5f) * 2);
            enemyHealthSlider.value = healthRatio;
        }

        private void CheckEnemyDead()
        {
            if (currentEnemyHealth <= 0)
            {
                Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        protected abstract void OnTriggerEnter();

        public void ReduceHealth(float amount) => currentEnemyHealth -= amount;
    }
}