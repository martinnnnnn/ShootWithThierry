using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    public ObjectPool bulletPool;
    
    public Sprite ammoSprite;


    public int startingAmmo = 10;
    private int currentAmmo;
    

    void Start()
    {
       
    }

    
    

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        GameObject bullet = bulletPool.GetPooledObject();
    //        bullet.transform.position = transform.position;
    //        bullet.transform.rotation = transform.rotation;
    //        bullet.GetComponent<SpriteRenderer>().sprite = ammoSprite;
    //        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bullet.GetComponent<BasicBullet>().speed, 0f);
    //        bullet.SetActive(true);
    //    }
    //}
    
}
