using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalEventsSO", menuName = "ScriptableObjects/Events/GlobalEventsSO")]
public class GlobalEventsSO : ScriptableObject
{
    public TimerEvents TimerEvents = new TimerEvents();
    public EnemyEvents EnemyEvents = new EnemyEvents();
    public WaveEvents WaveEvents = new WaveEvents();
}

public class TimerEvents
{
    public Action<int> RemainingTime;
}

public class EnemyEvents
{
    public Action OnEnemyDeath;
}

public class WaveEvents
{
    public Action<int> OnWaveStarted;
}