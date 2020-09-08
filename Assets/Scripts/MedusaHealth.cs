using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaHealth : MonoBehaviour
{
    Vector3 localScale;
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = MedusaScript.health;
        transform.localScale = localScale;
        if(MedusaScript.health == 0)
        {
            Destroy(gameObject);
        }
    }
}
