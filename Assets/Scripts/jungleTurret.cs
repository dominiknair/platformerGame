using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jungleTurret : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float agroRange;
    public GameObject bulletToRight;
    Vector2 bulletPos;
    [SerializeField]public float startTimeBeetweenShots;

    // [SerializeField] private AudioSource hurt;
    // [SerializeField] private AudioSource d2;
    

    private float timeBeetweenShots;
    // Start is called before the first frame update
    void Start()
    {
        timeBeetweenShots = startTimeBeetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        // print("distance to player: " + distToPlayer);
        
        if(distToPlayer < agroRange)
        {
            Debug.Log("player is close");

            bulletPos = transform.position;
        
            bulletPos += new Vector2 (+1f, 0.4f);
            if(timeBeetweenShots <=0)
            {
                Instantiate (bulletToRight, bulletPos, Quaternion.identity);
                timeBeetweenShots = startTimeBeetweenShots;
            }
            else{
                timeBeetweenShots -= Time.deltaTime;
            }  
        }
        else{
             Debug.Log("player is far");
        }
        
    }
    
}
