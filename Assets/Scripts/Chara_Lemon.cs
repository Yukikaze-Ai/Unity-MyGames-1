using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chara_Lemon : MonoBehaviour {
    public bool facing_left = true;
    public float Chara_Velocity;
    public float jump_force;

    private Rigidbody2D RG2D;
    private bool grounded = false;
    private bool jump = false;
    private Animator Anim;
    private Transform groundCheck;
    private bool OKornot_RUN = false;
    private bool OKornot_JUMP = false;
    private bool run = false;
    private float moveHori;
    private float speed;

    // Use this for initialization
    void Start () {
        RG2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Title"));
        if(grounded)
        {
            Anim.SetBool("B_jump", false);
            OKornot_JUMP = true;
            OKornot_RUN = true;
            
        }else
        {
            Anim.SetBool("B_jump", true);
            OKornot_JUMP = false;
            OKornot_RUN = false;
        }
        if (Input.GetButton("Jump") && OKornot_JUMP)
        {
            //print("jump" + Time.time);
            jump = true;
        }
        else
            jump = false;
        if (Input.GetButton("Horizontal") && OKornot_RUN)
            run = true;
        else
            run = false;

    }
    void Awake()
    {
        Anim = GetComponent<Animator>();
        groundCheck = transform.Find("Check_Ground");
    }

    void FixedUpdate()
    {
        
        speed = GetComponent<Rigidbody2D>().velocity.y;
        Anim.SetFloat("velocity", speed);
        moveHori = Input.GetAxis("Horizontal");
        if (Input.GetButton("Horizontal"))
            Anim.SetBool("AXI_X", true);
        else
            Anim.SetBool("AXI_X", false);
       // else
         //   Anim.SetBool("ATTACK", false);

        //float finish_anim= Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        AnimatorStateInfo stateinfo = Anim.GetCurrentAnimatorStateInfo(0);
        if (stateinfo.IsName("Base Layer.shooting"))
        {
            run = false;
            jump = false;
            moveHori = 0;
        }
        //running
        Run_v();

        if (jump)
        {
            RG2D.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            RG2D.AddForce(Vector2.up * jump_force);
        }
       

        if (moveHori > 0 && facing_left)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (moveHori < 0 && !facing_left)
            // ... flip the player.
            Flip();


    }
    void Flip()
    {
        facing_left = !facing_left;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void Run_v()
    {
        if (run)
        {
            Vector2 movement = new Vector2(moveHori * Chara_Velocity, GetComponent<Rigidbody2D>().velocity.y);
            RG2D.velocity = movement;
        }else
        {
            bool jumpornot = Anim.GetBool("B_jump");
            if (!jumpornot)
            {
                Vector2 movement = new Vector2(GetComponent<Rigidbody2D>().velocity.x * 0.5f, GetComponent<Rigidbody2D>().velocity.y);
                //Vector2 movement = new Vector2(0, 0);
                RG2D.velocity = movement;
            }
        }
    }
}
