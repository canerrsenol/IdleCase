using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalEventsSO", menuName = "ScriptableObjects/Events/GlobalEventsSO")]
public class GlobalEventsSO : ScriptableObject
{
    public TimerEvents TimerEvents = new TimerEvents();
}

public class TimerEvents
{
    public Action<int> RemainingTime;
}