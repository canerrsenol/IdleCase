using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private MovementSettingsSO movementSettings;
    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;
        moveDirection.Normalize();

        controller.Move(moveDirection * movementSettings.moveSpeed * Time.deltaTime);
    }
}