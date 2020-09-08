using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDrragonScript : enemy
{
    
    public float speed = 2.0f;
    public bool MoveToRight=true;
    private Collider2D c2d;
 
    protected override void Start()
    {
        base.Start();
        c2d = GetComponent<Collider2D>();
       
    }
    // Update is called once per frame
    private void Update()
    {
        if(MoveToRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0,0);
            transform.localScale = new Vector2(1,1);
        }   
        else{
            transform.Translate(-2 * Time.deltaTime * speed, 0,0);
            transform.localScale = new Vector3(-1,1);
        }
    }
    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.CompareTag("turn"))
        {
            if(MoveToRight)
            {
                MoveToRight = false;
            }
            else
            {
                MoveToRight = true;
            }
        }
    }
}
