﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2Script : enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask ground;
    private Collider2D c2d;
    private bool facingLeft = true;
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        c2d = GetComponent<Collider2D>();
       
    }

    // Update is called once per frame
    private void Update()
    {
        if(anim.GetBool("Jumping"))
        {
            if(rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }
        if(c2d.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling",false);
        }
    }
    private void Move()
    {
        if(facingLeft)
        {
            if(transform.position.x > leftCap)
            {

                if(transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1,1);
                }

                if(c2d.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping",true);
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

                if(transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1,1);
                }

                if(c2d.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping",true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }
    // public void JumpedOn()
    // {
    //     anim.SetTrigger("Death");
    // }
    // private void death()
    // {
    //     Destroy(this.gameObject);
    // }
}
