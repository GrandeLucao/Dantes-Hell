using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestEnemy : MonoBehaviour
{
    public float speed;
    public float walkTime;
    public int health;
    public int damage=1;

    private float atkTime;
    private float atkTimer;
    private float timer;
    private bool walkRight=true;


    private Rigidbody2D rig;
    private Animator anim;

    public GameObject cross;
    public Transform firepoint;


    void Start()
    {
        rig=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        atkTime=Random.Range(2f, 6f);

    }

    void Update()
    {
        CrossAtk();
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

    void CrossAtk()
    {
        StartCoroutine("Taca");
    }

    IEnumerator Taca()
    {        
        atkTimer+=Time.deltaTime;
        if(atkTimer>=atkTime)
        {            
            anim.SetInteger("transition", 3);
            GameObject Cross=Instantiate(cross,firepoint.position, firepoint.rotation);
            if(transform.rotation.y!=0)
            {
                Cross.GetComponent<Cross>().isRight=true;
            }
            if(transform.rotation.y==0)
            {
                Cross.GetComponent<Cross>().isRight=false;
            }  
            atkTimer=0f;
            yield return new WaitForSeconds(0.1f);
            anim.SetInteger("transition", 1);
            
               
        }
    }

    public void Damage(int dmg)
    {
        health-=dmg;        
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
        }
    }
}
