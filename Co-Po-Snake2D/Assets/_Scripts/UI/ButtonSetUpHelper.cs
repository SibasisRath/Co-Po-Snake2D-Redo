using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public static class ButtonSetUpHelper 
{
    public static void SetUpButton(Button button, UnityAction unityAction)
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners(); // Clear existing listeners

            button.onClick.AddListener(() => { unityAction?.Invoke(); });
        }
        else
        {
            Debug.Log($"Error: Button reference is null.");
        }
    }

    public static void SetUpButton(Button button, UnityAction<GameModes> unityAction, GameModes selectedGameMode)
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners(); // Clear existing listeners

            button.onClick.AddListener(() => { unityAction?.Invoke(selectedGameMode); });
        }
        else
        {
            Debug.Log($"Error: Button reference is null.");
        }
    }
}
