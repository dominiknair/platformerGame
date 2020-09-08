using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : enemy
{
    [SerializeField] Transform player;
    [SerializeField] float agroRange;
    [SerializeField] float moveSpeed;
    private bool facingLeft;

    public GameObject bulletToRight, bulletToLeft;
    private float timeBeetweenShots;

    [SerializeField]public float startTimeBeetweenShots;
    Vector2 bulletPos;
    public float fireRate = 2f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
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
        
        if(transform.position.x < player.position.x)
        {
            facingLeft = true;
            rb.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1,1);
            // transform.localScale = new Vector2(-1,1);
            fire();
        }
        else
        {
            facingLeft = false;
            rb.velocity = new Vector2(-moveSpeed, 0);
            // transform.localScale = new Vector2(-1,1);
            transform.localScale = new Vector2(1,1);
            fire();
        }
    }
    private void stopChasePlayer()
    {
        rb.velocity = new Vector2(0,0);
    } 
        void fire ()
    {
        bulletPos = transform.position;
        // shoot to the right 
        if (facingLeft == false)
        {
            bulletPos += new Vector2 (+0.0f, -0.02f);
            if(timeBeetweenShots <=0)
            {
                Instantiate (bulletToRight, bulletPos, Quaternion.identity);
                timeBeetweenShots = startTimeBeetweenShots;
            }
            else{
                timeBeetweenShots -= Time.deltaTime;
            }  
        } 
        else
        {
            // shoot to the left
            bulletPos += new Vector2 (-0.0f, -0.02f);
            if(timeBeetweenShots <=0)
            {
                Instantiate (bulletToLeft, bulletPos, Quaternion.identity);
                timeBeetweenShots = startTimeBeetweenShots;
            }
            else{
                timeBeetweenShots -= Time.deltaTime;
            }
        }
    }
}
