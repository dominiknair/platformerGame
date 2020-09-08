using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    Vector3 localScale;
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = JinnScript.health;
        transform.localScale = localScale;
        if(JinnScript.health == 0)
        {
            Destroy(gameObject);
        }
    }
}
