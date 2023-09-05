using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speed;
    public bool isRight;
    public int damage=2;


    void Start()
    {
        rig=GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2.5f);
    }


    void FixedUpdate()
    {
        if(isRight){
            rig.velocity= Vector2.right*speed;
        }
        else{
            rig.velocity= Vector2.left*speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            collision.GetComponent<PatrolEnemy>().Damage(damage);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag=="Enemy1")
        {
            collision.GetComponent<PriestEnemy>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
