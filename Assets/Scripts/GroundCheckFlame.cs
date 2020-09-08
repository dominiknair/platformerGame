using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckFlame : MonoBehaviour
{
    [SerializeField] GameObject flameFromFeet;
    // Start is called before the first frame update
    void onEnterTrigger2D(Collider collision)
    {
        if(collision.gameObject.tag.Equals("Wall"))
        {
            Instantiate(flameFromFeet, transform.position, flameFromFeet.transform.rotation);
        }
    }
}
