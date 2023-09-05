using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moement : MonoBehaviour
{
    public int health=3;
    public float speed;
    public float jumpForce;
    private bool isJumping;
    private bool isFire;
    
    private Rigidbody2D rig;
    private Animator anim; 

    public GameObject knife;
    public Transform hitbox;

    void Start()
    {
        rig=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        gameController.instance.UpdateLives(health);


    }


    void Update()
    {
        Jump();
        KnifeAtk();
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float movement= Input.GetAxis("Horizontal");
        rig.velocity=new Vector2(movement*speed, rig.velocity.y);
        if(movement>0 && !isJumping){
            transform.eulerAngles=new Vector3(0,0,0);
            anim.SetInteger("transition",1);
        }
        if(movement<0 && !isJumping){
            transform.eulerAngles=new Vector3(0,180,0);
            anim.SetInteger("transition",1);
        }
        if(movement==0 && !isJumping && !isFire){
            anim.SetInteger("transition",0);
        }

    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump")){
            if(!isJumping){
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isJumping=true;
                anim.SetInteger("transition", 2);
            }
        }
    }

    void KnifeAtk()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            isFire=true;
            anim.SetInteger("transition", 3);
            GameObject Knife=Instantiate(knife,hitbox.position, hitbox.rotation);
            FindObjectOfType<AudioManager>().Play("Knife");
            if(transform.rotation.y==0)
            {
                Knife.GetComponent<knife>().isRight=true;
            }
            if(transform.rotation.y==-180)
            {
                Knife.GetComponent<knife>().isRight=false;
            }
            yield return new WaitForSeconds(0.1f);
            anim.SetInteger("transition", 0);           
            isFire=false;
        }
    }

    public void Damage(int dmg)
    {
        health-=dmg;
        gameController.instance.UpdateLives(health);
        anim.SetTrigger("hit");
        FindObjectOfType<AudioManager>().Play("MainCharHit");
        if(transform.rotation.y==0)
            {
                transform.position+=new Vector3(-2f,0,0);
            }
            if(transform.rotation.y==180){
                transform.position+=new Vector3(2f,0,0);
            }
        if(health<=0)
        {
            gameController.instance.GameOver();
            FindObjectOfType<AudioManager>().Play("MainCharDeath");
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.layer==3)
        {
            isJumping=false;
        }

        if(coll.gameObject.layer==11)
        {
            gameController.instance.Level();
        }

        if(coll.gameObject.layer==9)
        {
            gameController.instance.GameOver();
        }

        if(coll.gameObject.layer==12)
        {
            gameController.instance.EndGame();
        }

    }
}
