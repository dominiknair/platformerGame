using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaHealthBar : MonoBehaviour
{
    Vector3 localScale;
    void Start()
    {
        localScale = transform.localScale;
    }
    
    // Update is called once per frame
    void Update()
    {
        localScale.x = GorillaScript.health;
        transform.localScale = localScale;
        if(GorillaScript.health == 0)
        {
            Destroy(gameObject);
        }
    }
}