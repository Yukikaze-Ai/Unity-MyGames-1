using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bow : MonoBehaviour
{
    //public Rigidbody2D arrow;              // Prefab of the rocket.
             // The speed the rocket will fire at.
    private Chara_Lemon playerCtrl;       // Reference to the PlayerControl script.
    public float speed;
    public Animator anim;
    // Use this for initialization
    public float start_time;
    public float delay_time;

    public GameObject arrow;
    private GameObject New_Arrow;
    void Awake()
    {
        playerCtrl = transform.root.GetComponent<Chara_Lemon>();
        anim = transform.root.gameObject.GetComponent<Animator>();

    }
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            //if (Input.GetButton("Fire3"))
                anim.SetBool("ATTACK", true);
                //anim.SetBool("ATTACK", true);
                //coroutine = WaitAndPrint(delay_time);
                //StartCoroutine(coroutine);
                InvokeRepeating("attack", start_time, delay_time);
        }
        else if (!Input.GetButton("Fire3"))
            anim.SetBool("ATTACK", false);


    }
    private void attack()
    {
        print("WaitAndPrint " + Time.time);
        // AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(1);
        //if (stateinfo.IsName("shooting.shooting"))
        //{

        if (playerCtrl.facing_left)
        {

            New_Arrow = (GameObject)Instantiate(arrow, transform.position, Quaternion.Euler(new Vector3(0, 0, 0f)));
            Rigidbody2D The_Arrow = New_Arrow.GetComponent<Rigidbody2D>();
            The_Arrow.velocity = new Vector2(-speed, 0);
            //Rigidbody2D arrow= GetComponent<>
           //Rigidbody2D bulletInstance = Instantiate(arrow, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
           //New_Arrow=
            //bulletInstance.velocity = new Vector2(-speed, 0);
        }
        else
        {
            //Rigidbody2D bulletInstance = Instantiate(arrow, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
            //bulletInstance.velocity = new Vector2(speed, 0);
            New_Arrow = (GameObject)Instantiate(arrow, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f)));
            Rigidbody2D The_Arrow = New_Arrow.GetComponent<Rigidbody2D>();
            The_Arrow.velocity = new Vector2(speed, 0);
        }


        //}
        if (!Input.GetButton("Fire3"))
        {
            
            CancelInvoke();
            anim.SetBool("ATTACK", false);
        }


    }

}