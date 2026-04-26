using Unity.VisualScripting;
using UnityEngine;

public class EnemySwordHit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player") && GameManager.instance.EnemyAttack)
    {
        Debug.Log("Enemy Hit Player");

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(10f);
        }

        GameManager.instance.EnemyAttack = false;
    }
}

    
}
