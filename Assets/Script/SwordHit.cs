using UnityEngine;

public class SwordHit : MonoBehaviour
{
    public float damage = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyLvl1"))
        {
            Debug.Log("Attack");

            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                Debug.Log("Hit Enemy");
                if(GameManager.instance.PlayerAttack == true)
                {
                    enemy.TakeDamage(GameManager.instance.AttackDamage + 20); // Use damage from GameManager
                    GameManager.instance.PlayerAttack = false; // reset attack state
                }
            }
        }
    }
}