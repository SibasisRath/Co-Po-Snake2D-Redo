using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    [Space]
    [Header("Scriptable Object")]
    [SerializeField] private SoundInfo soundInfo;
    // Start is called before the first frame update
    void Start()
    {
        // sound slider SetUp
        backGroundSoundSlider.gameObject.SetActive(true);
        soundEffectSlider.gameObject.SetActive(true);
        backGroundSoundSlider.value = soundInfo.BackGroundSound;
        soundEffectSlider.value = soundInfo.SoundEffect;

        // panel management
        mainPanel.SetActive(false);
        howToPlayPanel.SetActive(false);

        //button setup
        SetUpButton(backToMainButton, BackToMainButtonClicked);
        SetUpButton(howToPlayButton, HowToPlayButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        backGroundSoundSlider.onValueChanged.AddListener((float value) => { soundInfo.BackGroundSound = backGroundSoundSlider.value;});
        soundEffectSlider.onValueChanged.AddListener((float value) => { soundInfo.SoundEffect = soundEffectSlider.value; });
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
