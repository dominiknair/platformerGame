using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaScript : enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    private bool facingLeft = true;

    [SerializeField] private float jumpLength =10f;
    [SerializeField] private float jumpHeight=15f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private AudioSource hurt;

    private Collider2D c2d;
    [SerializeField] public static int health;
    [SerializeField] private int health2;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        c2d = GetComponent<Collider2D>();
        health = health2;
        Debug.Log(health);
    }

    // Update is called once per frame
    private void Update()
    {
        health = health2;
        if(anim.GetBool("Jump"))
        {
            if(rb.velocity.y < .1)
            {
                anim.SetBool("Fall", true);
                anim.SetBool("Jump", false);
            }
        }
        if(c2d.IsTouchingLayers(ground) && anim.GetBool("Fall"))
        {
            anim.SetBool("Fall",false);
        }
    }
    private void Move()
    {
        if(facingLeft)
        {
            if(transform.position.x > leftCap)
            {
                if(transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1,1);
                }
                if(c2d.IsTouchingLayers(ground))
                {
                    anim.SetBool("Jump",true);
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            if(transform.position.x < rightCap)
            {
                if(transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1,1);
                }
                if(c2d.IsTouchingLayers(ground))
                {
                    anim.SetBool("Jump",true);
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }
    public override void JumpedOn()
    {
        if(health2==0)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            // deathSound.Play();
            anim.SetTrigger("Death");
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            GetComponent<Collider2D>().enabled=false;
        }
        
            hurt.Play();
            health2 = health2 - 1;
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(ChangeColour(1));
            Debug.Log(health2);
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
    public int getHealth()
    {
        return health;
    }
}
