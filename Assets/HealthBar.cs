using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] int playerHealth = 100;
    TMP_Text healthText;

    bool isDeath = false;

    public bool GetIsDeath()
    {
        return isDeath;
    }

    private void Start()
    {
        healthText = GetComponent<TMP_Text>();
    }

    public void IncreaseHealth(int amountOfIncrease)
    {
        playerHealth += amountOfIncrease;

        if (playerHealth > 100)
            playerHealth = 100;

        UpdateHealthText();
    }

    public void DecreaseHealth(int amountOfDecrease)
    {
        playerHealth -= amountOfDecrease;

        if (playerHealth < 0)
        {
            playerHealth = 0;
            isDeath = true;
        }
        
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = $"Score: {playerHealth}";
    }
}
