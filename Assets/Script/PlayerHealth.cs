using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public float maxHealth = 100f;
    private float currentHealth;

    public Image healthBarFill; // UI Image

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        GameManager.instance.playerHealth = currentHealth;

        UpdateHealthUI(); // 🔥 Important
    }

    // ================= DAMAGE =================
    public void TakeDamage(float damage)
    {
        if (GameManager.instance.isSheldUp)
        {
            Debug.Log("Attack Blocked");
            return;
        }

        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI();

        GameManager.instance.playerHealth = currentHealth;

        Debug.Log("Player Took Damage: " + damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ================= HEAL =================
    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthUI();

        GameManager.instance.playerHealth = currentHealth;

        Debug.Log("Player Healed: " + amount);
    }

    // ================= UI UPDATE =================
    void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    // ================= DEATH =================
    void Die()
    {
        Debug.Log("Player Died");

        // Add your logic:
        // Animator, respawn, game over UI etc.
    }
}