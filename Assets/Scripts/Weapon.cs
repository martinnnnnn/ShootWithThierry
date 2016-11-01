using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    
    public Sprite image;
    public GameObject bulletPrefab;


    public int startingAmmo = 10;
    private int currentAmmo;
    

    void Start()
    {
       
    }

    
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
    
}
