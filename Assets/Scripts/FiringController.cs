//using UnityEngine;
//using System.Collections;



//public enum FIRING_TYPE
//{
//    BASIC,
//    MONSTER,
//    FLAME_THROWER,
//    SHOTGUN
//}


//public class FiringController : MonoBehaviour
//{

//    private delegate void FiringMethod(float deltaTime);
//    FiringMethod currentFiringMethod;
//    FIRING_TYPE firingType;

//    public void InitFiring(FIRING_TYPE fType)
//    {
//        firingType = fType;
//        switch(firingType)
//        {
//            case FIRING_TYPE.BASIC:
//                currentFiringMethod = basicFiring;
//                break;
//            case FIRING_TYPE.MONSTER:
//                currentFiringMethod = monsterFiring;
//                break;
//            case FIRING_TYPE.FLAME_THROWER:
//                currentFiringMethod = flameThrowerFiring;
//                break;
//            case FIRING_TYPE.SHOTGUN:
//                currentFiringMethod = shotgunFiring;
//                break;
//            default:
//                currentFiringMethod = basicFiring;
//                break;

//        }
//    }



//    void Update()
//    {
//        currentFiringMethod(Time.deltaTime);
//    }

//    private void basicFiring(float deltaTime)
//    {
//        currentFiringDelay += Time.deltaTime;
//        if (Input.GetButton("Fire1") && primaryWeapon.bulletsLeft > 0 && currentFiringDelay >= fireRate)
//        {
//            currentFiringDelay = 0f;
//            --primaryWeapon.bulletsLeft;
//            float horizontal = Input.GetAxis("Horizontal2");
//            float vertical = Input.GetAxis("Vertical2");
//            if (horizontal != 0 || vertical != 0)
//            {
//                GameObject go = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
//                go.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * bulletSpeed, vertical * bulletSpeed);

//                float deg = Vector2.Angle(new Vector2(1, 0), new Vector2(horizontal, vertical));
//                if (vertical < 0)
//                {
//                    //    deg = 360 - deg;
//                }

//                go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, go.transform.eulerAngles.y, deg);
//                Physics2D.IgnoreCollision(go.GetComponent<Collider2D>(), GetComponent<Collider2D>());
//            }
//        }
//    }

//    private void monsterFiring(float deltaTime)
//    {

//    }

//    private void flameThrowerFiring(float deltaTime)
//    {

//    }

//    private void shotgunFiring(float deltaTime)
//    {

//    }

//}
