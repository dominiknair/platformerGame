using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLock : MonoBehaviour
{
    
    void Update()
    {
        if(MedusaScript.health == 0)
        {
            Destroy(gameObject);
        }
    }
}
