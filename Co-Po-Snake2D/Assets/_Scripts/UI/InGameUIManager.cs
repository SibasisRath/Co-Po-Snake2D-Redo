using UnityEngine;
using UnityEngine.UI;
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
        panel.SetActive(false);
        mainMessage.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        ButtonSetUpHelper.SetUpButton(mainButton, BackToMainButtonClicked);
        ButtonSetUpHelper.SetUpButton(restartButton, RestartButtonClicked);
        ButtonSetUpHelper.SetUpButton(resumeButton, ResumeButtonClicked);
        mainButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GameStateManager.GameState == GameStates.Running)
        {
            panel.SetActive(false);
        }
        if (GameStateManager.GameState == GameStates.Pause)
        {
            OnPause();
        }
        if (GameStateManager.GameState == GameStates.GameOver)
        {
            OnGameOver();
        }
    }

    private void RestartButtonClicked()
    {
        GameStateManager.GameState = GameStates.Running;
        SceneManagerScript.Instance.SceneLoading(ScenesEnum.GameScene);
    }

    private void BackToMainButtonClicked()
    {
        SceneManagerScript.Instance.SceneLoading(ScenesEnum.MainScene);
    }

    private void ResumeButtonClicked()
    {
        GameStateManager.GameState = GameStates.Running;
        panel.SetActive(false);
        mainMessage.gameObject.SetActive(false);
        mainButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
    }

    private void OnPause()
    {
        GameStateManager.GameState = GameStates.Pause;
        panel.SetActive(true);
        mainMessage.gameObject.SetActive(true);
        mainMessage.text = "Pause";
        mainButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(true);
    }

    private void OnGameOver()
    {
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
