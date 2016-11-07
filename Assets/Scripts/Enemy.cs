using UnityEngine;
using System.Collections;




public class Enemy : MonoBehaviour
{

    public Transform Player;
    public float MoveSpeed = 4;
    public float MaxDist = 10;
    public float MinDist = 5;




    void Start()
    {

    }

    void Update()
    {
        // transform.LookAt(Player);

        //Vector3 diff = Player.position - transform.position;
        //diff.Normalize();

        //float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        //if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        //{

        //    transform.position += transform.forward * MoveSpeed * Time.deltaTime;



        //    if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
        //    {
        //        //Here Call any function U want Like Shoot at here or something
        //    }

        //}

        transform.LookAt(Player.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation


        //move towards the player
        if (Vector3.Distance(transform.position, Player.position) > MinDist)
        {//move if distance from target is greater than 1
            transform.Translate(new Vector3(MoveSpeed * Time.deltaTime, 0, 0));

            //if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
        //    {
        //        //Here Call any function U want Like Shoot at here or something
        //    }
        }
    }

}
