using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private MovementSettingsSO movementSettings;
    [SerializeField] private ShootController shootController;
    [SerializeField] private Transform characterVisualTransform;
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
        if (!canMove) return;

        Vector3 moveDirection = new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical);
        moveDirection.y = 0;
        controller.Move(moveDirection * movementSettings.moveSpeed * Time.deltaTime);

        if(!shootController.HasTarget() && moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            characterVisualTransform.rotation = Quaternion.Slerp(
                characterVisualTransform.rotation,
                targetRotation,
                movementSettings.rotationSpeed * Time.deltaTime
            );
        }

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