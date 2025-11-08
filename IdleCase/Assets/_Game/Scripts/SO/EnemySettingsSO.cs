using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettingsSO", menuName = "ScriptableObjects/EnemySettingsSO")]
public class EnemySettingsSO : ScriptableObject
{
    public float moveSpeed = 3.5f;
    public float attackRange = 2.0f;
    public float attackDamage = 10f;
    public float acceleration = 8f;
    public float angularSpeed = 120f;
    public int maxHealth = 100;
}
