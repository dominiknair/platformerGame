using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    public GameObject uiObject;
    [SerializeField] private string sceneName;
    
    void Start()
    {
        uiObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController player = collision.gameObject.GetComponent<playerController>();
        int chipCount = player.getChips();
        if(collision.gameObject.tag =="Player")
        {
            if(chipCount ==4)
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                uiObject.SetActive(true);
                StartCoroutine(textDisappear(3));
            }
        }
    }
    private IEnumerator textDisappear(int expireyTime)
    {
        yield return new WaitForSeconds(expireyTime);
        uiObject.SetActive(false);
    }
}
