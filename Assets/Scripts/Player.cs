using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    
    public float maxSpeed = 10f;
    protected bool facingRight = true;

    protected Animator anim;

    protected bool grounded = false;
    public Transform groundCheck;
    protected float groundRaduis = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700f;
    
    
    protected Rigidbody2D myRigidBody;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    

    protected virtual void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRaduis, whatIsGround);
        

        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);


        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("hSpeed", Mathf.Abs(move));
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);




        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

    }

    virtual protected void Update()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
    }

    

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
