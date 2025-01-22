using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public Image healthBar;
    private float healthAmount = 100f;

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }

    public float GetHealth() { return healthAmount; }
}
