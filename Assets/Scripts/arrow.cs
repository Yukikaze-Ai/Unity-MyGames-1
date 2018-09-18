using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float speed = 20f;
    public int damage;
//    private IEnumerator coroutine;


    void Start()
    {
        //The_arrow = GetComponent<Rigidbody2D>();
        Destroy(gameObject,10);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            // ... find the Enemy script and call the Hurt function.
            col.gameObject.GetComponent<Enemy>().Hurt(damage);
            Destroy(gameObject);
            //coroutine = WaitAndPrint(0.03f);
            //StartCoroutine(coroutine);


        }
        else if (col.tag == "Walls")
        {
            Destroy(gameObject);
            //coroutine = WaitAndPrint(0.03f);
            //StartCoroutine(coroutine);
        }
        else if (col.tag == "Ground")
        {
            Destroy(gameObject);
            //coroutine = WaitAndPrint(0.03f);
            //StartCoroutine(coroutine);
        }



    }
    private IEnumerator WaitAndPrint(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
