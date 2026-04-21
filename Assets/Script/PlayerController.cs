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

        float run = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;

        Vector3 move = new Vector3(moveX, 0, moveZ);

        if (move.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(move);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        anim.SetFloat("MoveX", moveX * run, 0.15f, Time.deltaTime);
        anim.SetFloat("MoveY", moveZ * run, 0.15f, Time.deltaTime);
    }
}