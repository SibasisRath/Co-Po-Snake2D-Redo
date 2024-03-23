using UnityEngine;
using UnityEngine.UI;

public class GameModeSelectorUIManager : MonoBehaviour
{
    [Header("panel")]
    [SerializeField] private GameObject mainPanel;
    [Space]
    [Header("Buttons")]
    [SerializeField] private Button backToMainButton;
    [SerializeField] private Button singleModeButton;
    [SerializeField] private Button copoModeButton;

    // Start is called before the first frame update
    void Start()
    {
        mainPanel.SetActive(false);

        backToMainButton.gameObject.SetActive(true);
        singleModeButton.gameObject.SetActive(true);
        copoModeButton.gameObject.SetActive(true);

        ButtonSetUpHelper.SetUpButton(backToMainButton, BackToMainButtonIsClicked);
        ButtonSetUpHelper.SetUpButton(singleModeButton, ModeButtonIsClicked, GameModes.SinglePlayer);
        ButtonSetUpHelper.SetUpButton(copoModeButton, ModeButtonIsClicked, GameModes.CopoPlayer);
    }

    private void BackToMainButtonIsClicked()
    {
        mainPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void ModeButtonIsClicked(GameModes selectedGameMode)
    {
        GameHandler.Mode = selectedGameMode;
        SceneManagerScript.Instance.SceneLoading(ScenesEnum.GameScene);
    }
}
