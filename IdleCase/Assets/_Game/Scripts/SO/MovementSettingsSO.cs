using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettingsSO", menuName = "ScriptableObjects/MovementSettingsSO")]
public class MovementSettingsSO : ScriptableObject
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
}
