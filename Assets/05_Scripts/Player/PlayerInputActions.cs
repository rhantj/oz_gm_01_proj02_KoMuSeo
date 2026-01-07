using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputActions : MonoBehaviour
{
    public InputSystem_Actions InputActions { get; private set; }
    public PlayerController pctrl;

    private void Awake()
    {
        InputActions = new InputSystem_Actions();
        pctrl = GetComponent<PlayerController>();

        InputActions.Player.Move.performed += pctrl.OnMoveInput;
        InputActions.Player.Move.canceled += pctrl.OnMoveInputCanceled;
        InputActions.Player.Jump.started += pctrl.OnJumpInput;
        InputActions.Player.Crouch.performed += pctrl.OnCrouchInput;
        InputActions.Player.Crouch.canceled += pctrl.OnCrouchInputCanceled;
        InputActions.Player.Attack.performed += pctrl.OnFireInput;
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}