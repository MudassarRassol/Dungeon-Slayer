using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    
    public float maxHealth = 100f;
    public float currentHealth;

    public Image healthFill; // UI Fill Image
    
    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        anim = GetComponent<Animator>();
    }



    public void TakeDamage(float damage)
    {
        if(GameManager.instance.PlayerAttack == true)
        {   
            currentHealth -= damage;
        }

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth == 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        healthFill.fillAmount = currentHealth / maxHealth;
    }

void Die()
{
    Debug.Log("Enemy Died");

    anim.SetTrigger("Death");

    // 👇 ADD THIS LINE (spawn potion)
    if (PotionsPowers.instance != null)
    {
        float chance = Random.value;

        if (chance <= 0.5f) // 50% drop chance
        {
            PotionsPowers.instance.SpawnPotionPower(transform.position);
        }
    }

    Destroy(gameObject, 2f);
}
}