using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private MovementSettingsSO movementSettings;
    private CharacterController controller;
    private PlayerAnimationController animationController;
    private bool canMove = true;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animationController = GetComponent<PlayerAnimationController>();
    }

    public void SetMovementControllerState(bool state)
    {
        floatingJoystick.gameObject.SetActive(state);
        canMove = state;
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical);
        moveDirection.y = 0;
        controller.Move(moveDirection * movementSettings.moveSpeed * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            animationController.SetAnimationState(PlayerAnimationState.Running);
        }
        else
        {
            animationController.SetAnimationState(PlayerAnimationState.Idle);
        }
    }
}