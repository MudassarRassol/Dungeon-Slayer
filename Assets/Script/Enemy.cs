using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;

    public float chaseRange = 10f;
    public float attackRange = 2f;

    private NavMeshAgent agent;
    private Animator anim;

    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    void Start()
    {
        
        // ✅ FIRST get components
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // ✅ THEN configure agent
        agent.stoppingDistance = attackRange;

        anim.SetBool("isWalking", false);
    }

    void Update()
    {
        player = GameManager.instance.player; // 🔥 FIXED: get player from GameManager instead of FindWithTag
        if (player == null) return; // safety

        float distance = Vector3.Distance(transform.position, player.position);

        // ---------------- CHASE ----------------
        if (distance > attackRange && distance <= chaseRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            anim.SetBool("isWalking", true);
        }

        // ---------------- ATTACK ----------------
        else if (distance <= attackRange)
        {
            agent.isStopped = true;

            // smooth face player
            Vector3 lookPos = player.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);

            anim.SetBool("isWalking", false);

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;

                anim.SetTrigger("Attack");
                GameManager.instance.EnemyAttack = true;
            }
        }

        // ---------------- IDLE ----------------
        else
        {
            agent.isStopped = true;
            anim.SetBool("isWalking", false);
        }
    }
}