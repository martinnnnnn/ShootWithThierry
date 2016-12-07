using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class Hero : MonoBehaviour
{

    public Animator animBody;
    public Animator animArms;
    public Transform visuals;
    private bool facingLeft = true;

    private float HeroSpeed;
    private Rigidbody2D HeroRigidBody;

    [HideInInspector]
    public bool canMove;
    private float timeEnableMove;
    private float timeUnmovable = 0.2f;

    [HideInInspector]
    private int HeroLife;

    private float currentFiringDelay;

    public Animation death;

    void Start ()
    {
        HeroLife = GameDataManager.Instance.HeroStartingLife;
        HeroSpeed = GameDataManager.Instance.HeroSpeed;
        HeroRigidBody = GetComponent<Rigidbody2D>();
        canMove = true;
        UIManager.Instance.SetHeroLife(HeroLife);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float hMove = Input.GetAxis("Horizontal1");
        float vMove = Input.GetAxis("Vertical1");
        if (canMove)
        {
            HeroRigidBody.velocity = new Vector2(hMove * HeroSpeed, vMove * HeroSpeed);

            HandleAnim();

        }
    }



    void Update()
    {
        ArmsLookAt(new Vector3(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"), 0));

        Fire();
        if (!canMove)
        {
            if (timeEnableMove == 0)
            {
                timeEnableMove = Time.timeSinceLevelLoad + timeUnmovable;
            }
            else
            {
                if (Time.timeSinceLevelLoad > timeEnableMove)
                {
                    canMove = true;
                    timeEnableMove = 0;
                }
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D c)
    {
        LootSpawn lSpawn = c.gameObject.GetComponent<LootSpawn>();
        if (lSpawn)
        {
            if (lSpawn.CurrentLoot)
            {
                Loot loot = lSpawn.CurrentLoot.GetComponent<Loot>();
                if (loot)
                {
                    switch (loot.GetLootType())
                    {
                        case LOOT_TYPE.PISTOL:
                            SoundManager.Instance.PlaySound("Ammo_PickUp_1");
                            BulletManager.Instance.ChangeAmmo(loot.GetLootAmount());
                            break;
                        case LOOT_TYPE.SNIPER:
                            SoundManager.Instance.PlaySound("Ammo_PickUp_2");
                            UIManager.Instance.SetRecipe("SNIPER");
                            BulletManager.Instance.SetWeaponType(WEAPON_TYPE.SNIPER);
                            break;
                        case LOOT_TYPE.ROCKET:
                            SoundManager.Instance.PlaySound("Recipe_PickUp");
                            UIManager.Instance.SetRecipe("ROCKET");
                            BulletManager.Instance.SetWeaponType(WEAPON_TYPE.ROCKET);
                            break;
                        case LOOT_TYPE.LIFE:
                            if (HeroLife < GameDataManager.Instance.HeroStartingLife)
                            {
                                HeroLife++;
                            }
                            break;
                    }
                }
                lSpawn.CurrentLoot = null;
                DeapthManager.Instance.RemoveActor(loot.gameObject);
                loot.gameObject.SetActive(false);
            }
        }
    }


    void Fire()
    {
        currentFiringDelay += Time.deltaTime;
        bool isAttacking = false;

        if (Input.GetButton("Fire1") && BulletManager.Instance.CanFireGun(WEAPON_TYPE.PISTOL,currentFiringDelay))
        {
            currentFiringDelay = 0f;
            float horizontal = Input.GetAxis("Horizontal2");
            float vertical = Input.GetAxis("Vertical2");


            if (horizontal != 0 || vertical != 0)
            {
                isAttacking = true;
                SoundManager.Instance.PlaySound("Weapon_Pistol");
                BulletManager.Instance.FireBullet(WEAPON_TYPE.PISTOL, transform, new Vector2(horizontal, vertical));
            }
        }

        if (Input.GetButton("Fire2"))
        {
            if (BulletManager.Instance.CanFireGun(WEAPON_TYPE.SNIPER, currentFiringDelay))
            {
                currentFiringDelay = 0f;
                float horizontal = Input.GetAxis("Horizontal2");
                float vertical = Input.GetAxis("Vertical2");

                if (horizontal != 0 || vertical != 0)
                {
                    isAttacking = true;
                    SoundManager.Instance.PlaySound("Weapon_Sniper");
                    BulletManager.Instance.FireBullet(WEAPON_TYPE.SNIPER, transform, new Vector2(horizontal, vertical));
                }
            }
            else if (BulletManager.Instance.CanFireGun(WEAPON_TYPE.ROCKET, currentFiringDelay))
            {
                currentFiringDelay = 0f;
                float horizontal = Input.GetAxis("Horizontal2");
                float vertical = Input.GetAxis("Vertical2");

                if (horizontal != 0 || vertical != 0)
                {
                    isAttacking = true;
                    SoundManager.Instance.PlaySound("Weapon_Rocket");
                    BulletManager.Instance.FireBullet(WEAPON_TYPE.ROCKET, transform, new Vector2(horizontal, vertical));
                }
            }
        }
        if (animArms.gameObject.activeSelf) animArms.SetBool("isAttacking", isAttacking);
        //HandleAnim(isAttacking);
    }
    
    bool dead = false;
    public void ChangeLife(int amount)
    {
        
        HeroLife += amount;
        if (HeroLife > GameDataManager.Instance.HeroStartingLife)
        {
            HeroLife = GameDataManager.Instance.HeroStartingLife;
        }
        if (HeroLife <= 0)
        {
            //SceneManager.LoadScene("testTime");
            if (!dead)
            {
                dead = true;
                StartCoroutine(Death());
            }
        }
        if (amount < 0)
        {
            SoundManager.Instance.PlaySound("Hero_Hit_" + UnityEngine.Random.Range(1, 3));
        }
        else
        {
            SoundManager.Instance.PlaySound("Monster_Life_Up");
        }
        UIManager.Instance.SetHeroLife(HeroLife);

    }

    IEnumerator Death()
    {
        Debug.Log("death");
        animArms.gameObject.SetActive(false);
        animBody.SetTrigger("Death");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("testTime");

    }


    void HandleAnim()
    {
        float leftH = Input.GetAxis("Horizontal1");
        float leftV = Input.GetAxis("Vertical1");
        float rightH = Input.GetAxis("Horizontal2");
        float rightV = Input.GetAxis("Vertical2");

        float sendToBodyH = 0f;
        float sendToBodyV = 0f;

        if (Mathf.Abs(rightH) > .1f || Mathf.Abs(rightV) > 0.1f)
        {
            // si la vitesse de deplacement > 0 => changement pour shootingspeed
            // si la vitesse de deplacement < 0 => changement pour shootingspeed * .1
            if (Mathf.Abs(leftH) < 0.1f && Mathf.Abs(leftV) < 0.1f)
            {
                sendToBodyH = rightH * 0.1f;
                sendToBodyV = rightV * 0.1f;
            }
            else
            {
                sendToBodyH = rightH;
                sendToBodyV = rightV;
            }
        }
        else
        {
            sendToBodyH = leftH;
            sendToBodyV = leftV;
        }
        animBody.SetFloat("hSpeed", Mathf.Abs(sendToBodyH));
        animBody.SetFloat("vSpeed", sendToBodyV);

        if (sendToBodyV > 0 && Mathf.Abs(sendToBodyV) > Mathf.Abs(sendToBodyH))
        {
            animArms.gameObject.SetActive(false);
        }
        else
        {
            animArms.gameObject.SetActive(true);
            animArms.SetFloat("hSpeed", Mathf.Abs(sendToBodyH));
            animArms.SetFloat("vSpeed", sendToBodyV);
        }

        if (sendToBodyH > 0 && facingLeft)
        {
            Flip();
        }
        else if (sendToBodyH < 0 && !facingLeft)
        {
            Flip();
        }
    }


    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = visuals.localScale;
        theScale.x *= -1;
        visuals.localScale = theScale;
    }

    void ArmsLookAt(Vector3 direction)
    {
        //if (direction != Vector3.zero)
        //{
        //    Quaternion rotation = Quaternion.LookRotation
        //         (direction - animArms.transform.position, animArms.transform.TransformDirection(Vector3.up));
        //    animArms.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        //}
    }

    private void AnimateDash()
    {
        animBody.SetTrigger("Dash");
        animArms.gameObject.SetActive(false);
    }


}
