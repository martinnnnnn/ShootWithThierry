using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class HeroMenu : MonoBehaviour
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

    void Start ()
    {
        HeroSpeed = 20;
        HeroRigidBody = GetComponent<Rigidbody2D>();
        canMove = true;
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

    private void AnimateDash()
    {
        animBody.SetTrigger("Dash");
        animArms.gameObject.SetActive(false);
    }


}
