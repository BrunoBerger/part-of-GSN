using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public float speed = 1;
    public int chord = 0;
    public int beerCounter = 1;
    public int beersColectedTotal = 0;
    public int maxCombo = 0;
    public int danceRush = 0;
    public float police = 0;
    public float policeChange = 0;
    private States _currentState = States.Playing;
    public UnityEvent stateChanged = new UnityEvent();

    public void SetState(States state)
    {
        _currentState = state;
        stateChanged.Invoke();
    }
}

public enum States
{
    Playing,
    End
}
