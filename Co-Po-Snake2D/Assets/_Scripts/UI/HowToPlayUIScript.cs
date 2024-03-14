using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

        SetUpButton(leftButton,LeftButtonClicked);
        SetUpButton(rightButton, RightButtonClicked);
        SetUpButton(backButton, BackButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpButton(Button button, UnityAction unityAction)
    {
        if (button != null)
        {
            button.onClick.AddListener(()=> { unityAction?.Invoke(); });
        }
        else
        {
            Debug.Log($"error: {button} reference is null.");
        }
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
        for (int i = 0; i < instructions.Count; i++)
        {
            if (i == counter)
            {
                instructions[i].SetActive(true);
            }
            else
            {
                instructions[i].SetActive(false);
            }
        }
        
    }

}
