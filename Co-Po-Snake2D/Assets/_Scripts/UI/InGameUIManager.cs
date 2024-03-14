using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    //[SerializeField] private GameHandler gameHandler;
    [Space]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI mainMessage;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Button mainButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button resumeButton;
    
    private void Start()
    {
        //gameHandler.State = GameStates.Start;
        panel.SetActive(false);
        mainMessage.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        SetUpButton(mainButton, BackToMainButtonClicked);
        SetUpButton(restartButton, RestartButtonClicked);
        SetUpButton(resumeButton, ResumeButtonClicked);
        mainButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GameHandler.State == GameStates.Pause)
        {
            OnPause();
        }

        if (GameHandler.State == GameStates.GameOver)
        {
            OnGameOver();
        }

    }

    private void SetUpButton(Button button, UnityAction unityAction)
    {
        if (button != null)
        {
            button.onClick.AddListener(() => {
                //SoundManager.Instance.Play(Sounds.ButtonClick);
                unityAction?.Invoke();
            });
        }
        else
        {
            Debug.Log($"{button} is null.");
        }
    }

    private void RestartButtonClicked()
    {
        SceneManagerScript.Instance.SceneLoading(ScenesEnum.GameScene);
    }

    private void BackToMainButtonClicked()
    {
        SceneManagerScript.Instance.SceneLoading(ScenesEnum.MainScene);
    }


    private void ResumeButtonClicked()
    {
        GameHandler.State = GameStates.Resume;
        panel.SetActive(false);
        mainMessage.gameObject.SetActive(false);
        mainButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
    }

    private void OnPause()
    {
        GameHandler.State = GameStates.Pause;
        panel.SetActive(true);
        mainMessage.gameObject.SetActive(true);
        mainMessage.text = "Pause";
        mainButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(true);
    }

    private void OnGameOver()
    {
        //GameHandler.State = GameStates.End;
        panel.SetActive(true);
        mainMessage.gameObject.SetActive(true);
        mainMessage.text = "GameOver";
        description.gameObject.SetActive(true);



        if (GameHandler.GameResult.Item1)
        {
            description.text = GameHandler.GameResult.Item2 + " please do not kill yourself.";
        }
        else
        {
            description.text = GameHandler.GameResult.Item2 + " Congratulation.";
        }
        mainButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
}
