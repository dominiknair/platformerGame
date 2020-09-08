using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : enemy
{
//     public float speed;
//     public float stoppingDistance;
//     private Transform target;
    [SerializeField] Transform player;
    [SerializeField] float agroRange;
    [SerializeField] float moveSpeed;
 
    Rigidbody2D rb;
    
    protected override void Start()
    {
        base.Start();
        // Enemy = GameObject.FindGameObjectWithTag ("Enemy");
        // target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Vector2.Distance(transform.position, target.position) > stoppingDistance){
        //     transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        // }
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        // print("distance to player: " + distToPlayer);
        
        if(distToPlayer < agroRange)
        {
            chasePlayer();
        }
        else
        {
            stopChasePlayer();
        }
    }
    private void chasePlayer()
    {
        anim.SetBool("fly",true);
        if(transform.position.x < player.position.x)
        {
            
            rb.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1,1);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1,1);
        }
    }
    private void stopChasePlayer()
    {
        anim.SetBool("fly",false);
        rb.velocity = new Vector2(0,0);
    } 
    public override void JumpedOn()
    {
        Destroy(this.gameObject);
    }
    
}
