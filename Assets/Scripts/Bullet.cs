using UnityEngine;
using System.Collections;



public class Bullet : MonoBehaviour
{


    public float speed = 10f;
    public WEAPON_TYPE weaponType;
    public int damage = 1;

    private Rigidbody2D myRigidBody;
    private SpriteRenderer myRenderer;
    private delegate void MoveMethod();
    MoveMethod currentMoveMethod;

    //void OnAwake()
    //{
    //    myRigidBody = GetComponent<Rigidbody2D>();
    //    myRenderer = GetComponent<SpriteRenderer>();
    //}

    void OnEnable()
    {
       // Debug.Log("rotation : " + transform.rotation);
        myRigidBody = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        switch (weaponType)
        {
            case WEAPON_TYPE.BASIC:
                
                currentMoveMethod = BasicMove;
                myRenderer.sprite = Resources.Load<Sprite>("IMG_" + WEAPON_TYPE.BASIC.ToString());
                break;
            default:
                myRenderer.sprite = Resources.Load<Sprite>("IMG_BASIC");
                currentMoveMethod = BasicMove;
                break;
        }

        //Destroy(gameObject.GetComponent<BoxCollider2D>());
        //gameObject.AddComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        currentMoveMethod();

        
    }


    public void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log("CONTACT");
        Enemy enemy = c.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            Debug.Log("ENEMY");
            enemy.LoseLife(damage);
        }
        
    }


    void BasicMove()
    {
        myRigidBody.AddForce(Vector3.forward * speed);
    }
}
