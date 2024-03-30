using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    [Header("Panels")] 
    [SerializeField] private GameObject mainPannel;
    [SerializeField] private GameObject playModeSelectorPannel;
    [SerializeField] private GameObject settingsPannel;
    [Space]
    [Header("Main Panel buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; // while replying games the game changes the scene to lobby scene with timeScale value 0. To avoid that this line is here.
        mainPannel.SetActive(true);
        playModeSelectorPannel.SetActive(false);
        settingsPannel.SetActive(false);

        playButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);

        ButtonSetUpHelper.SetUpButton(playButton, PlayButtonClicked);
        ButtonSetUpHelper.SetUpButton(settingsButton, SettingsButtonClicked);
        ButtonSetUpHelper.SetUpButton(quitButton, QuitButtonClicked);
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void PlayButtonClicked()
    {
        mainPannel.SetActive(false);
        playModeSelectorPannel.SetActive(true); 
    }

    public void SettingsButtonClicked()
    {
        mainPannel.SetActive(false);
        settingsPannel.SetActive(true);
    }
}
