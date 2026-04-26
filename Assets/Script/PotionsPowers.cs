using UnityEngine;
using System.Collections;

public class PotionsPowers : MonoBehaviour
{
    public enum PotionType { Red, Blue, Green }
    public PotionType potionType;

    public static PotionsPowers instance;

    public GameObject[] potionsPowers;

    private Coroutine speedCoroutine;
    private Coroutine attackCoroutine;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        switch (potionType)
        {
            case PotionType.Red:
                ApplyAttackBoost();
                break;

            case PotionType.Blue:
                ApplySpeedBoost();
                break;

            case PotionType.Green:
                PlayerHealth.instance.Heal(20f); // 🔥 FIXED
                Debug.Log("Green Potion Used");
                break;
        }

        Destroy(gameObject);
    }

    // ================= ATTACK BOOST =================
    void ApplyAttackBoost()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            GameManager.instance.AttackDamage -= 5f;
        }
        Debug.Log("Red Potion Used");
        GameManager.instance.AttackDamage += 5f;
        attackCoroutine = StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(10f);

        GameManager.instance.AttackDamage -= 5f;
        attackCoroutine = null;
    }

    // ================= SPEED BOOST =================
    void ApplySpeedBoost()
    {
        if (speedCoroutine != null)
        {
            StopCoroutine(speedCoroutine);
            GameManager.instance.speed -= 1f;
        }

        GameManager.instance.speed += 1f;
        speedCoroutine = StartCoroutine(ResetSpeed());
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(10f);

        GameManager.instance.speed -= 1f;
        speedCoroutine = null;
    }

    // ================= SPAWN SYSTEM =================
    public void SpawnPotionPower(Vector3 position)
    {
        if (potionsPowers.Length == 0) return;

        int randomIndex = Random.Range(0, potionsPowers.Length);

        Vector3 spawnPos = position + Vector3.up * 1f;

        Instantiate(potionsPowers[randomIndex], spawnPos, Quaternion.identity);
    }
}