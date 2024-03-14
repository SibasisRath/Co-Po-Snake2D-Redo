using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameModeSelectorUIManager : MonoBehaviour
{
    [Header("panel")]
    [SerializeField] private GameObject mainPanel;
    [Space]
    [Header("Buttons")]
    [SerializeField] private Button backToMainButton;
    [SerializeField] private Button singleModeButton;
    [SerializeField] private Button copoModeButton;
    [Space]
    [Header("Serialize Object")]
    [SerializeField] private GameModeManager modeManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("mode selection.");
        mainPanel.SetActive(false);

        backToMainButton.gameObject.SetActive(true);
        singleModeButton.gameObject.SetActive(true);
        copoModeButton.gameObject.SetActive(true);

        SetUpButton(backToMainButton, BackToMainButtonIsClicked);
        SetUpButton(singleModeButton, SingleModeButtonIsClicked);
        SetUpButton(copoModeButton, CopoModeButtonIsClicked);
    }

    public void SetUpButton(Button button, UnityAction unityAction)
    {
        if (button != null)
        {
            //Sound
            button.onClick.AddListener(() => { unityAction?.Invoke(); });
        }
        else
        {
            Debug.Log($"error: {button} is null.");
        }
    }

    private void BackToMainButtonIsClicked()
    {
        mainPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void SingleModeButtonIsClicked()
    {
        modeManager.GameMode = GameModes.SinglePlayer;
        Debug.Log($"mode selection.{modeManager.GameMode}");
        SceneManagerScript.Instance.SceneLoading(ScenesEnum.GameScene);
    }

    private void CopoModeButtonIsClicked()
    {
        modeManager.GameMode = GameModes.CopoPlayer;
        Debug.Log($"mode selection.{modeManager.GameMode}");
        SceneManagerScript.Instance.SceneLoading(ScenesEnum.GameScene);
    }
}
