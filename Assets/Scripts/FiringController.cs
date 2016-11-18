using UnityEngine;
using System.Collections;



public enum FIRING_TYPE
{
    BASIC,
    MONSTER,
    FLAME_THROWER,
    SHOTGUN
}


public class FiringController : MonoBehaviour
{

    private GameObject bulletPrefab;

    private delegate void FiringMethod(float deltaTime);
    FiringMethod currentFiringMethod;

    private float currentFiringDelay;

    private FIRING_TYPE firingType;
    private float basicFireRate;
    private float specialFireRate;
    private int basicAmmoValue;
    private int specialAmmoValue;
    private float bulletSpeed;


    public void InitFiring(FIRING_TYPE fType, float delay, int ammo, GameObject prefab, float speed)
    {
        firingType = fType;
        basicFireRate = delay;
        bulletPrefab = prefab;
        bulletSpeed = speed;
        if (fType == FIRING_TYPE.BASIC)
        {
            basicAmmoValue += ammo;
        }
        else
        {
            specialAmmoValue = ammo;
        }

        switch (firingType)
        {
            case FIRING_TYPE.BASIC:
                currentFiringMethod = basicFiring;
                break;
            case FIRING_TYPE.MONSTER:
                currentFiringMethod = monsterFiring;
                break;
            case FIRING_TYPE.FLAME_THROWER:
                currentFiringMethod = flameThrowerFiring;
                break;
            case FIRING_TYPE.SHOTGUN:
                currentFiringMethod = shotgunFiring;
                break;
            default:
                currentFiringMethod = basicFiring;
                break;

        }
    }



    void Update()
    {
        currentFiringMethod(Time.deltaTime);
    }

    private void basicFiring(float deltaTime)
    {
        currentFiringDelay += Time.deltaTime;
        if (Input.GetButton("Fire1") && basicAmmoValue > 0 && currentFiringDelay >= basicFireRate)
        {
            currentFiringDelay = 0f;
            --basicAmmoValue;
            float horizontal = Input.GetAxis("Horizontal2");
            float vertical = Input.GetAxis("Vertical2");

            if (horizontal != 0 || vertical != 0)
            {
                GameObject go = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                Vector2 direction = new Vector2(horizontal, vertical);
                direction.Normalize();
                go.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed);

                float deg = Vector2.Angle(new Vector2(1, 0), new Vector2(horizontal, vertical));

                go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, go.transform.eulerAngles.y, deg);
            }
        }
    }

    private void monsterFiring(float deltaTime)
    {

    }

    private void flameThrowerFiring(float deltaTime)
    {
        basicFiring(deltaTime);

    }

    private void shotgunFiring(float deltaTime)
    {
        basicFiring(deltaTime);

    }

}
