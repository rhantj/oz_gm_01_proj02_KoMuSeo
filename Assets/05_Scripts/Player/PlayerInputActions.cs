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
        InputActions.Player.Move.canceled += pctrl.OnMoveInputCanceld;
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