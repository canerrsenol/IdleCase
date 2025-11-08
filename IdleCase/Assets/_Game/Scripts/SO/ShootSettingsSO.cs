using UnityEngine;

[CreateAssetMenu(fileName = "ShootSettingsSO", menuName = "ScriptableObjects/ShootSettingsSO")]
public class ShootSettingsSO : ScriptableObject
{
    public float shootCooldown = 0.15f;
    public float checkAreaRadius = 0.5f;
}
