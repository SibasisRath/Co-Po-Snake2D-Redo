using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : GenericSingletonScript<SceneManagerScript>
{
    private int sceneNumber;

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
