using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public float speed;
    public float walkTime;
    public int health;
    public int damage=1;


    private float timer;
    private bool walkRight=true;


    private Rigidbody2D rig;
    private Animator anim;


    void Start()
    {
        rig=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();

    }


    void FixedUpdate()
    {
        timer+=Time.deltaTime;
        if(timer>=walkTime)
        {
            walkRight=!walkRight;
            timer=0f;
        }
        if(walkRight)
        {
            transform.eulerAngles=new Vector2(0,180);
            rig.velocity=Vector2.right*speed;
            anim.SetInteger("transition", 1);
        }
        else
        {
            transform.eulerAngles=new Vector2(0,0);
            rig.velocity=Vector2.left*speed;
            anim.SetInteger("transition", 1);
        }
    }

    public void Damage(int dmg)
    {
        health-=dmg;
        anim.SetTrigger("hit");
        FindObjectOfType<AudioManager>().Play("EnemyHit");
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<moement>().Damage(damage);
            anim.SetInteger("transition", 2);
        }
    }
}
