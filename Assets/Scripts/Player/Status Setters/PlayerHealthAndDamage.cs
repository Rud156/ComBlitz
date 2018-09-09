using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthAndDamage : MonoBehaviour
{
    [Header("Player Health")]
    public float maxPlayerHealth;

    public GameObject destroyEffect;

    [Header("UI Display")]
    public Color minHealthColor = Color.red;

    public Color halfHealthColor = Color.yellow;
    public Color maxHealthColor = Color.green;
    public Slider playerHealthSlider;
    public Image playerHealthFiller;

    private float currentPlayerHealth;

    private void Start() => currentPlayerHealth = maxPlayerHealth;

    private void Update()
    {
        UpdateHealthToUI();
        CheckPlayerDead();
    }

    private void UpdateHealthToUI()
    {
        float maxHealth = maxPlayerHealth;
        float currentHealthLeft = currentPlayerHealth;
        float healthRatio = currentHealthLeft / maxHealth;

        if (healthRatio <= 0.5)
            playerHealthFiller.color = Color.Lerp(minHealthColor, halfHealthColor, healthRatio * 2);
        else
            playerHealthFiller.color = Color.Lerp(halfHealthColor, maxHealthColor, (healthRatio - 0.5f) * 2);
        playerHealthSlider.value = healthRatio;
    }

    private void CheckPlayerDead()
    {
        if (currentPlayerHealth <= 0)
        {
            GameObject destroyEffectInstance =
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = destroyEffectInstance.GetComponent<ParticleSystem>();
            ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
            shapeModule.skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();

            Destroy(gameObject);
        }
    }
}