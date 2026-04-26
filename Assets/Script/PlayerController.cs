using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed ;
    private Animator anim;
    private string[] Ibuttonattack = new string[] { "SW1", "SW2" };
    private string[] Obuttonattack = new string[] { "SW3", "SW4", "SW5", "SW6" };
    private string[] Kbuttonattack = new string[] { "LG1", "LG2" };
    private string Shield = "SH";
    private bool isAttacking = false;

    void Start()
    {   
        speed = GameManager.instance.speed; // Initialize speed from GameManager
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();

        if(GameManager.instance.playerHealth <= 0)
        {
            anim.SetTrigger("Death");
            // Optionally, disable player controls here
            this.enabled = false; // disables this script to prevent further input
        }
    }

    // ---------------- MOVEMENT ----------------

void HandleMovement()
{
    // ✅ Freeze movement, rotation AND animation blending while attacking
    if (isAttacking)
    {
        anim.SetFloat("MoveX", 0f, 0.15f, Time.deltaTime);
        anim.SetFloat("MoveY", 0f, 0.15f, Time.deltaTime);
        return; // ⛔ skip everything below (no translate, no rotation)
    }

    float moveX = Input.GetAxis("Horizontal");
    float moveZ = Input.GetAxis("Vertical");
    float run = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;
    Vector3 move = new Vector3(moveX, 0, moveZ);

    if (move.magnitude > 0.1f)
    {
        // ✅ Rotation is inside this block — only rotates when actually moving
        transform.rotation = Quaternion.LookRotation(move);
        transform.Translate(Vector3.forward * speed * run * Time.deltaTime);
    }

    anim.SetFloat("MoveX", moveX * run, 0.15f, Time.deltaTime);
    anim.SetFloat("MoveY", moveZ * run, 0.15f, Time.deltaTime);
}

    // ---------------- ATTACK ----------------
    void HandleAttack()
    {
        if (isAttacking) return;

        if (Input.GetKeyDown(KeyCode.I))
            TriggerAttack(Ibuttonattack);
        else if (Input.GetKeyDown(KeyCode.O))
            TriggerAttack(Obuttonattack);
        else if (Input.GetKeyDown(KeyCode.Space))
            anim.SetTrigger("jumb");
        else if (Input.GetKeyDown(KeyCode.K))
            TriggerAttack(Kbuttonattack);
        else if (Input.GetKeyDown(KeyCode.L))
            StartAttack(Shield);
    }

    void TriggerAttack(string[] attacks)
    {
        int randomIndex = Random.Range(0, attacks.Length);
        StartAttack(attacks[randomIndex]);
    }

    void StartAttack(string triggerName)
    {
        GameManager.instance.PlayerAttack = true; // set attack state   
        if(triggerName == Shield) GameManager.instance.isSheldUp = true; // set shield state
        isAttacking = true;
        ResetAllTriggers();
        anim.SetTrigger(triggerName);

        // ⚠️ Replace this with AnimationEvent on the clip for accuracy
        Invoke(nameof(EndAttack), 1f); // adjust '1f' to match your animation length
    }

    void EndAttack()
    {
        if(GameManager.instance.isSheldUp == true) GameManager.instance.isSheldUp = false; // reset shield state
        isAttacking = false;
        GameManager.instance.PlayerAttack = false; // reset attack state
    }

    void ResetAllTriggers()
    {
        foreach (string t in Ibuttonattack) anim.ResetTrigger(t);
        foreach (string t in Obuttonattack) anim.ResetTrigger(t);
        foreach (string t in Kbuttonattack) anim.ResetTrigger(t);
        anim.ResetTrigger(Shield);
    }
}