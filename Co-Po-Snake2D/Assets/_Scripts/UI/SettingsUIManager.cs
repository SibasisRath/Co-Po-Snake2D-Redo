using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    [Header("panel")]
    [SerializeField] private GameObject mainPanel;
    [SerializeReference] private GameObject howToPlayPanel;
    [Space]
    [Header("slider")]
    [SerializeField] private Slider backGroundSoundSlider;
    [SerializeField] private Slider soundEffectSlider;
    [Space]
    [Header("Button")]
    [SerializeField] private Button backToMainButton;
    [SerializeField] private Button howToPlayButton;

    // Start is called before the first frame update
    void Start()
    {
        // sound slider SetUp
        backGroundSoundSlider.gameObject.SetActive(true);
        soundEffectSlider.gameObject.SetActive(true);

        // panel management
        mainPanel.SetActive(false);
        howToPlayPanel.SetActive(false);

        //button setup
        ButtonSetUpHelper.SetUpButton(backToMainButton, BackToMainButtonClicked);
        ButtonSetUpHelper.SetUpButton(howToPlayButton, HowToPlayButtonClicked);
    }
    private void BackToMainButtonClicked()
    {
        mainPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }
    private void HowToPlayButtonClicked()
    {
        howToPlayPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
