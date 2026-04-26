using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform player;
    public bool PlayerAttack = false;

    public float playerHealth ;

    public float speed = 2f;

    public float AttackDamage = 10f;

    public bool EnemyAttack = false;

    public bool isSheldUp = false;
    public float PlayerSpeed ;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
