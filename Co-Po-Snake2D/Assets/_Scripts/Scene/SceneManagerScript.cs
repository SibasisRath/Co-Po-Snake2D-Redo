using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private static SceneManagerScript instance;


    private int sceneNumber;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static SceneManagerScript Instance { get => instance; private set => instance = value; }

    public void SceneLoading(ScenesEnum scene)
    {        
        switch (scene)
        {
            case ScenesEnum.MainScene:
                sceneNumber = 0; //main scene index
                break;
            case ScenesEnum.GameScene:
                sceneNumber = 1; //game scene index
                break;
            default:
                break;
        }
        SceneManager.LoadScene(sceneNumber);
    }
}
