using StateController;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(PlayerContext), typeof(PlayerInputActions))]
public class PlayerController : MonoBehaviour
{
    public PlayerContext playerCtx;
    public PlayerInputActions inputAction;
    public StateName PrevMovementState { get; set; } = StateName.Move;

    public Vector3 inputDir;
    public Vector3 mouseDelta;
    float pitch = 0;
    public bool isSprinting = false;
    public bool ISGROUNDED;
    public bool isJump = false;
    public bool isCrouching = false;
    public bool isFiring = false;

    private void Awake()
    {
        playerCtx = GetComponent<PlayerContext>();
        inputAction = GetComponent<PlayerInputActions>();
    }

    private void Update()
    {
        OnMouseInput();
        ISGROUNDED = playerCtx.CharacterController.isGrounded;
    }

    public void OnMouseInput()
    {
        mouseDelta = Mouse.current.delta.ReadValue();
        float mouseX = mouseDelta.x * playerCtx.Seneitivity;
        float mouseY = mouseDelta.y * playerCtx.Seneitivity;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -85f, 85f);
        playerCtx.PlayerCamera.localRotation = Quaternion.Euler(pitch, 0, 0);
        playerCtx.CharacterController.transform.Rotate(Vector3.up * mouseX);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputDir = new Vector3(input.x, 0, input.y);
    }

    public void OnMoveInputCanceled(InputAction.CallbackContext context)
    {
        inputDir = Vector3.zero;
    }

    public void OnSprintInput(InputAction.CallbackContext context)
    {
        if (inputDir != Vector3.zero)
            isSprinting = true;
    }

    public void OnSprintInputCanceled(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (!context.started || !playerCtx.CharacterController.isGrounded) return;
        isJump = true;
    }

    public void OnCrouchInput(InputAction.CallbackContext context)
    {
        if(context.interaction is PressInteraction)
        {
            isCrouching = !isCrouching;
            return;
        }

        isCrouching = true;
    }

    public void OnCrouchInputCanceled(InputAction.CallbackContext context)
    {
        if(context.interaction is HoldInteraction)
        {
            isCrouching = false;
        }
    }

    public void OnFireInput(InputAction.CallbackContext context)
    {
        isFiring = true;
    }

    public void OnFireInputCanceled(InputAction.CallbackContext context)
    {
        isFiring = false;
    }
}