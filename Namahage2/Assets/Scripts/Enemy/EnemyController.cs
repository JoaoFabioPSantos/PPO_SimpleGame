using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Animator anim;
    public object Object;
    public GameObject triggerArea;
    public GameObject hotArea;

    //stts do inimigo
    public int life = 3;

    void Start()
    {

        anim = GetComponent<Animator>();

    }

    private void EnemyDead()
    {
        anim.SetTrigger("Dead");
        Destroy(transform.gameObject.GetComponent<BoxCollider2D>());
        Destroy(transform.gameObject.GetComponent<Rigidbody2D>());
    }

    private void EnemyDeads()
    {
        Destroy(transform.gameObject.GetComponent<HotZoneCheck>());
        //  DestroyObject(triggerArea);
        // DestroyObject(hotArea);
        Destroy(transform.gameObject.GetComponent<SpriteRenderer>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            life -= 1;
            anim.SetBool("Hit", true);
        }

        if (life == 0)
        {
            anim.SetTrigger("Dead"); 
            EnemyDead();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            anim.SetBool("Hit", false);
        }
    }

}
