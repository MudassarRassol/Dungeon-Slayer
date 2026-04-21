using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float run = Input.GetKey(KeyCode.LeftShift) ? 10f : 5f;

        Vector3 move = new Vector3(moveX, 0, moveZ);

        if (move.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(move);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        anim.SetFloat("MoveX", moveX * run, 0.15f, Time.deltaTime);
        anim.SetFloat("MoveY", moveZ * run, 0.15f, Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Attack");
            anim.SetTrigger("SW1");
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Attack2");
            anim.SetTrigger("SW2");
        }

            if(Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Attack3");
                anim.SetTrigger("SW3");
            }

            if(Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log("Shield Attack");
                anim.SetTrigger("SA1");
            }

            if(Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("Shield Attack2");
                anim.SetTrigger("SA2");
            }

             if(Input.GetKeyDown(KeyCode.Semicolon))
            {
                Debug.Log("Shield Attack3");
                anim.SetTrigger("SW3");
            }
    }
}