using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
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
