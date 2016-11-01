using UnityEngine;
using System.Collections;

public class BasicBullet : MonoBehaviour
{

    
    public float speed = 20f;



    void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
    }

    void Update()
    {
        
    }
    
}
