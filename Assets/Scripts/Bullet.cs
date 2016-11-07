using UnityEngine;
using System.Collections;



public class Bullet : MonoBehaviour
{


    public float speed = 100f;

    private Rigidbody2D myRigidBody;
    private delegate void MoveMethod();
    MoveMethod currentMoveMethod;
    private WEAPON_TYPE weaponType;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        switch(weaponType)
        {
            case WEAPON_TYPE.BASIC:
                currentMoveMethod = BasicMove;
                break;
            default:
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
