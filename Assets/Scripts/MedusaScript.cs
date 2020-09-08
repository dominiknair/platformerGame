using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaScript : enemy
{
    [SerializeField] Transform player;
    [SerializeField] float agroRange;
    [SerializeField] float moveSpeed;

    [SerializeField]public float startTimeBeetweenShots;

    // [SerializeField] private AudioSource hurt;
    // [SerializeField] private AudioSource d2;
    

    private float timeBeetweenShots;
 
    Rigidbody2D rb;
    public GameObject bulletToRight, bulletToLeft;
    
    Vector2 bulletPos;
    public float fireRate = 2f;
    public bool facingRight = true;
    public static int health =5;



    protected override void Start()
    {
        base.Start();
        // Enemy = GameObject.FindGameObjectWithTag ("Enemy");
        // target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        timeBeetweenShots = startTimeBeetweenShots;
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
            facingRight = true;
            rb.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1,1);
            fire();
        }
        else
        {
            facingRight = false;    
            rb.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1,1);
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
        if (facingRight == true)
        {
            bulletPos += new Vector2 (+1f, -0f);
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
            bulletPos += new Vector2 (-1f, -0f);
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
    public override void JumpedOn()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if(health == 0)
        {
            // d2.Play();
            GetComponent<SpriteRenderer>().color = Color.white;
            // deathSound.Play();
            anim.SetTrigger("Death");
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            GetComponent<Collider2D>().enabled=false;
        }
        if(distToPlayer < agroRange)
        {
            // hurt.Play();
            health = health - 1;
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(ChangeColour(1));
        }
    }
    public override void death()
    {   
        Destroy(this.gameObject);
    }
    private IEnumerator ChangeColour(int expireyTime)
    {
        yield return new WaitForSeconds(expireyTime);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
