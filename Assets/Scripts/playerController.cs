using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public static playerController pc;
    private Rigidbody2D rb;
    private Animator anim;
    private enum State{idle, running, jumping, falling, hurt, shoot}
    private State state = State.idle;
    private Collider2D coll;
    public GameObject bulletToRight, bulletToLeft;
    
    Vector2 bulletPos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;
        
        
    [SerializeField] private LayerMask ground;

    [SerializeField] private int coins=0;
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private float hurtForce =1f;
    [SerializeField] private float jumpForce =4f;
    [SerializeField] private int health;
    [SerializeField] private TextMeshProUGUI healthAmountText;
    [SerializeField] private TextMeshProUGUI computerChipCount;

    [SerializeField] private AudioSource coin;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource shot;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource jumpPowerupSound;
    [SerializeField] private AudioSource healthRestoreSound;
    
    public bool facingRight = true;
    public bool doubleJump = true;    
    private GameMaster gm;

    [SerializeField] private int computerChip =0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        healthAmountText.text = health.ToString();
        gm=GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCP;
       
    }
    private void Update()
    {
        if(state != State.hurt)
        {
            Movement();
        }
        
        VelocityState();
        anim.SetInteger("state", (int)state);

        if(Input.GetButtonDown ("Fire1") && Time.time > nextFire && state != State.hurt)
        {
            if(pauseMenu.gamePaused==false)
            {
                shot.Play();
                state = State.shoot;
                nextFire = Time.time + fireRate;
                fire();
            }
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        if(hDirection <0)
        {
            // Debug.Log("left");
            facingRight = false;
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1,1);
            
        }
        else if(hDirection > 0 )
        {   
            // Debug.Log("Right");
            facingRight = true;
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1,1);
            
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        // small jump if press jump key
        // if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        // {
        //     jumpSound.Play();
        //     rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //     state = State.jumping;
        // }
        
        if(Input.GetButtonDown("Jump"))
        {   
            // Single jump
            if(coll.IsTouchingLayers(ground))
            {
                jumpSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                state = State.jumping;
                doubleJump = true;
                
            }
            else
            {
                // double jump
                if(doubleJump)
                {
                    jumpSound.Play();
                    doubleJump=false;
                    rb.velocity = new Vector2(rb.velocity.x, (jumpForce/1.75f));
                    state = State.jumping;
                }
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "collectable")
        {
            coin.Play();
            Destroy(collision.gameObject);
            coins = coins + 1; 
            count.text = coins.ToString();
        }
        if(collision.tag == "HealthPowerup" && health != 5)
        {
            healthRestoreSound.Play();
            Destroy(collision.gameObject);
            health = health + 1;
            healthAmountText.text = health.ToString();
            
        }
         if(collision.tag == "ComputerChip")
        {
            jumpPowerupSound.Play();
            computerChip = computerChip + 1;
            Destroy(collision.gameObject);
            computerChipCount.text = computerChip.ToString();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        enemy enemy = other.gameObject.GetComponent<enemy>();
        if(other.gameObject.tag == "Enemy")
        {
            if(state == State.falling)
            {
                enemy.JumpedOn();
               
                // Destroy(other.gameObject);
            }
            else
            {
                hurtSound.Play();
                state = State.hurt;
                
                decreaseHealth();// Method call to subtract health and update UI
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity= new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity= new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
        // if statement checks if player is on moving platform. If so it becomes child of that object
        if(other.gameObject.tag == "Platform")
        {
            
            this.transform.parent = other.transform;
        }
        // if(other.gameObject.tag == "BoostBox")
        // {
            
        //     this.transform.parent = other.transform;
        // }
        // If player touches spikes
        if(other.gameObject.tag == "Spikes")
        {
            hurtSound.Play();
            decreaseHealth();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(ResetPower(1));
        }
        if(other.gameObject.tag == "Electricity")
        {
            hurtSound.Play();
            decreaseHealth();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            GetComponent<SpriteRenderer>().color = Color.blue;
            StartCoroutine(ResetPower(1));
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        // if statement checks if player exited moving platform, if so stop being child of that object
        if(other.gameObject.tag == "Platform")
        {
            this.transform.parent = null;
        }
        // if(other.gameObject.tag == "BoostBox")
        // {
        //     this.transform.parent = null;
        // }
    }
    
    public void decreaseHealth()
    {
        health = health - 1;
        healthAmountText.text = health.ToString();
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void decreaseHealthFromShot()
    {
        hurtSound.Play();
        
        GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(ResetPower(1));
        decreaseHealth();
    }

    private void Footstep()
    {
        // if player is on the ground then play footsteps sound
        if(coll.IsTouchingLayers(ground))
        {
            footstep.Play();
        }
    }
        
    private void VelocityState()
    {
        // Check if player is jumping and if Y velocity if almost 0, switch to the fall animation
        if(state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        // Check if player is touching the ground, if so switch to the idle animation
        else if(state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        // check if player velocity is almost 0, and switch from hurt animation to idle
        else if(state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        // if velocity is greater than 2f then switch to running animation
        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        // if not anything else than idle animation
        else
        {
            state = State.idle;
        }
        
    }

    void fire ()
    {
        bulletPos = transform.position;
        // shoot to the right 
        if (facingRight == true)
        {
            bulletPos += new Vector2 (+1f, -0.1f);
            Instantiate (bulletToRight, bulletPos, Quaternion.identity); 
        } 
        else
        {
            // shoot to the left
            bulletPos += new Vector2 (-1f, -0.1f);
            Instantiate (bulletToLeft, bulletPos, Quaternion.identity);
        }

    }
    // Timer for how long jump powerup lasts for.
    private IEnumerator ResetPower(int expireyTime)
    {
        yield return new WaitForSeconds(expireyTime);
        jumpForce = 7f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public int getChips()
    {
        return computerChip;
    }
    
}
