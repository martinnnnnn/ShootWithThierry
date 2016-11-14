using UnityEngine;
using System.Collections.Generic;


public enum ENEMY_TYPE
{
    BASIC,
    CLEANER,
    FAT,
    GORDON
}


public class Enemy : MonoBehaviour
{
        
    public Transform Target;
    public float MoveSpeed = 4;
    public float MaxDist = 10;
    public float MinDist = 5;

    public float visionRadius = .1f;
    public LayerMask newTarget;

    void Start()
    {

    }

    void Update()
    {

        transform.LookAt(Target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        //Collider2D newTarg = Physics2D.OverlapCircle(transform.position, visionRadius, newTarget);
        //if (newTarg)
        //{
        //    Debug.Log("hello");
        //    Target = newTarg.gameObject.transform;
        //}
        

        if (Vector3.Distance(transform.position, Target.position) > MinDist)
        {
            transform.Translate(new Vector3(MoveSpeed * Time.deltaTime, 0, 0));

            //if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
        //    {
        //        //Here Call any function U want Like Shoot at here or something
        //    }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);


    }

}
