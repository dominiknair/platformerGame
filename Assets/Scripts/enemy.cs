using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    [SerializeField] protected AudioSource deathSound; 

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void JumpedOn()
    {
        // deathSound.Play();
        anim.SetTrigger("Death");
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled=false;
    }
    public virtual void death()
    {   
        Destroy(this.gameObject);
    }
}
