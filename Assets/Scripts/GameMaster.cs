using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCP;

    Scene m_Scene;

    string sceneName;

    void Awake()
    {
        if(instance == null)
        {
            instance=this;
            // COMMENT BELOW OUT FOR CHECKPOINTS DISBLAED
            // DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        Debug.Log(sceneName);
    }
}
