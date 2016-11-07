using UnityEngine;
using System.Collections;



public class Bullet : MonoBehaviour
{


    public float speed = 500f;
    public WEAPON_TYPE weaponType;

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
        myRigidBody.AddRelativeForce(Vector3.forward * speed);
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
    }

    void FixedUpdate()
    {
        currentMoveMethod();

        
    }


    void BasicMove()
    {
        myRigidBody.AddRelativeForce(Vector3.forward * speed);
    }
}
