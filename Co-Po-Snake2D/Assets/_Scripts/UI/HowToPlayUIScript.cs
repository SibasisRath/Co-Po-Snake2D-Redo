using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayUIScript : MonoBehaviour
{
    [Header("panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private List<GameObject> instructions;
    [Space]
    [Header("Buttons")]
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button backButton;

    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        UpdateInstructionSlides();
        settingsPanel.SetActive(false);
        
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

        ButtonSetUpHelper.SetUpButton(leftButton,LeftButtonClicked);
        ButtonSetUpHelper.SetUpButton(rightButton, RightButtonClicked);
        ButtonSetUpHelper.SetUpButton(backButton, BackButtonClicked);
    }

    private void BackButtonClicked()
    {
        settingsPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void LeftButtonClicked() { counter--; UpdateInstructionSlides(); }
    private void RightButtonClicked() { counter++; UpdateInstructionSlides(); }

    private void UpdateInstructionSlides()
    {
        if (counter == instructions.Count)
        {
            counter = 0;
        }
        else if (counter < 0)
        {
            counter = instructions.Count-1;
        }
        for (int counterIndex = 0; counterIndex < instructions.Count; counterIndex++)
        {
            if (counterIndex == counter)
            {
                instructions[counterIndex].SetActive(true);
            }
            else
            {
                instructions[counterIndex].SetActive(false);
            }
        }
        
    }

}
