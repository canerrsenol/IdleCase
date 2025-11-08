using UnityEngine;

[CreateAssetMenu(fileName = "BulletSettingsSO", menuName = "ScriptableObjects/BulletSettingsSO")]
public class BulletSettingsSO : ScriptableObject
{
    public float damage = 10f;
    public float speed = 20f;
    public float lifetime = 2f;
}
