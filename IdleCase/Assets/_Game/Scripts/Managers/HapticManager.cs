//using Lofelt.NiceVibrations;
using UnityEngine;

public class HapticManager : MonoBehaviour
{
    [SerializeField] private GlobalEventsSO globalEventsSO;

    private void OnEnable()
    {
        //globalEventsSO.HapticEvents.OnHaptic += Haptic;
    }

    private void OnDisable()
    {
        //globalEventsSO.HapticEvents.OnHaptic -= Haptic;
    }
    
    private void Start()
    {
        //LofeltHaptics.Initialize();
    }

    // public void Haptic(HapticPatterns.PresetType hapticType)
    // {
    //     if(!SaveLoad.I.playerProgress.hapticsOn) return;
    //     HapticPatterns.PlayPreset(hapticType);
    // }
}