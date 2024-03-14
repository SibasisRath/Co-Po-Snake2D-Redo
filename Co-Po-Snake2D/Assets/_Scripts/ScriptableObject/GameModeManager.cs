using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GameMode")]
public class GameModeManager : ScriptableObject
{
    [SerializeField] private GameModes gameMode;

    public GameModes GameMode { get => gameMode; set => gameMode = value; }
}
