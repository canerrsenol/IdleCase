using Cinemachine;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public bool IsHardLevel;
    public bool IsTutorialLevel;
    public int GoldToCollect = 50;
    public float TotalSecond = 60f;

    void Start()
    {
        // // Increase the priority of the virtual camera to ensure it is active
        // if (virtualCamera != null)
        // {
        //     virtualCamera.Priority = 12; // Set a high priority to make sure this camera is active
        // }
    }
}
