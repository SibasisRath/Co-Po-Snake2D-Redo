using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeCounter;
    [SerializeField] private float maxTime;
    [SerializeField] private float speed;
    [SerializeField] private bool canPerform;

    public bool CanPerform { get => canPerform; set => canPerform = value; }
    public float TimeCounter { get => timeCounter; set => timeCounter = value; }
    public float MaxTime { get => maxTime; set => maxTime = value; }
    public float Speed { get => speed; set => speed = value; }

    // Start is called before the first frame update
    void Start()
    {
        TimeCounter = MaxTime;
        CanPerform = true;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter += Time.deltaTime * Speed;
        //canPerform = false;
        if (TimeCounter > MaxTime)
        {
            CanPerform = true;
            TimeCounter -= MaxTime;
        }
    }
}
