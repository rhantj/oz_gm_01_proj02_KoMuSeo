using UnityEngine;

public class PlayerInputActions : MonoBehaviour
{
    public InputSystem_Actions InputActions { get; private set; }
    public PlayerController pctrl;

    private void Awake()
    {
        InputActions = new InputSystem_Actions();
        pctrl = GetComponent<PlayerController>();

        InputActions.Player.Move.performed += pctrl.OnMoveInput;
        InputActions.Player.Move.canceled  += pctrl.OnMoveInputCanceled;

        InputActions.Player.Sprint.performed += pctrl.OnSprintInput;
        InputActions.Player.Sprint.canceled  += pctrl.OnSprintInputCanceled;

        InputActions.Player.Jump.started += pctrl.OnJumpInput;

        InputActions.Player.Crouch.performed += pctrl.OnCrouchInput;
        InputActions.Player.Crouch.canceled  += pctrl.OnCrouchInputCanceled;

        InputActions.Player.Attack.performed += pctrl.OnFireInput;
        InputActions.Player.Attack.canceled  += pctrl.OnFireInputCanceled;

        InputActions.Player.Previous.performed += pctrl.OnMainWeaponInput;

        InputActions.Player.Next.performed += pctrl.OnSubWeaponInput;

        InputActions.Player.FireModeChange.started += pctrl.OnModeInput;
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