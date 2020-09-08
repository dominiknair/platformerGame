using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Collision2D other;

	public float velX = 5f;
	float velY = 0f;
	Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
    	rb = GetComponent<Rigidbody2D> ();
        
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2 (velX, velY);
        Destroy(gameObject,2f);

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        playerController player = other.gameObject.GetComponent<playerController>();
        if(other.gameObject.tag == "Player")
        {
                player.decreaseHealthFromShot();
                // Destroy(other.gameObject);
                Destroy(gameObject);
                
            
        }
         if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Platform" || other.gameObject.tag == "PlayerBullet")
        {
                Destroy(gameObject);
        }
        
    }
}