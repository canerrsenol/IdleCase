using System;
//using Lofelt.NiceVibrations;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalEventsSO", menuName = "ScriptableObjects/Events/GlobalEventsSO")]
public class GlobalEventsSO : ScriptableObject
{
    public HapticEvents HapticEvents = new HapticEvents();
    public UIEvents UIEvents = new UIEvents();
}

public class HapticEvents
{
    //public Action<HapticPatterns.PresetType> OnHaptic;
}

public class UIEvents
{
    public Action<int> RemainingTime;
}