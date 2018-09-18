using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 2f;        // The speed the enemy moves at.
    public int HP = 30;                  // How many times the enemy can be hit before it dies.
    public Sprite deadEnemy;            // A sprite of the enemy when it's dead.
    public Sprite damagedEnemy;         // An optional sprite of the enemy when it's damaged.
    public AudioClip[] deathClips;      // An array of audioclips that can play when the enemy dies.
    public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.

    //private SpriteRenderer ren;         // Reference to the sprite renderer.
    private Transform frontCheck;       // Reference to the position of the gameobject used for checking if something is in front.
    private bool dead = false;          // Whether or not the enemy is dead.
    //private bool move_Hor = false;
    private Animator Anim;
    private float velocity_now;
    private IEnumerator coroutine;

    //private bool 


    void Awake()
    {
        // Setting up the references.
        //ren = GetComponent<SpriteRenderer>();
        frontCheck = transform.Find("FrontCheck").transform;
        Anim = GetComponent<Animator>();
        //score = GameObject.Find("Score").GetComponent<Score>();
    }
    void FixedUpdate()
    {
        if (HP <= 0 && !dead)
        {
            Death();
        }

            // Create an array of all the colliders in front of the enemy.
           // Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);

        // Check each of the colliders.
        /*foreach (Collider2D c in frontHits)
        {
            // If any of the colliders is an Obstacle...
            if (c.tag == "Walls" )
            {
                // ... Flip the enemy and stop checking the other colliders.
                Flip();
                break;
            }
        }
        */
        // Set the enemy's velocity to moveSpeed in the x direction.
        if (!dead)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed * -1, GetComponent<Rigidbody2D>().velocity.y);
            velocity_now = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x);
            Anim.SetFloat("MOVE", velocity_now);
        }else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);


    }
    void OnTriggerEnter2D(Collider2D col)
    {
       if (col.tag == "Walls")
        {
            Flip();
        }
        else if (col.tag == "Ground")
        {
            Flip();
        }
    }
    public void Hurt(int hure_value)
    {
        // Reduce the number of hit points by one.
        HP = HP - hure_value;
        Anim.SetTrigger("HIT");
    }
    public void Flip()
    {
        // Multiply the x component of localScale by -1.
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }
    void Death()
    {
        dead = true;
        Anim.SetBool("die", true);
        coroutine = WaitAndPrint(0.5f);
        StartCoroutine(coroutine);
    }
    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
    }

}