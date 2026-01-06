using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerContext), typeof(PlayerInputActions))]
public class PlayerController : MonoBehaviour
{
    public PlayerContext playerCtx;
    public PlayerInputActions inputAction;

    public Vector3 inputDir;
    public Vector3 mouseDelta;
    float pitch = 0;

    private void Awake()
    {
        playerCtx = GetComponent<PlayerContext>();
        inputAction = GetComponent<PlayerInputActions>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputDir = new Vector3(input.x, 0, input.y);
    }

    public void OnMoveInputCanceld(InputAction.CallbackContext context)
    {
        inputDir = Vector3.zero;
    }

    public void LookAt(Vector3 direction)
    {
        if (direction.sqrMagnitude <= 0.01f) return;
        Quaternion targetAngle = Quaternion.LookRotation(direction);
        transform.rotation = targetAngle;
    }

    public void OnMouseInput(InputAction.CallbackContext context)
    {
        mouseDelta = Mouse.current.delta.ReadValue();
        float mouseX = mouseDelta.x * playerCtx.Seneitivity;
        float mouseY = mouseDelta.y * playerCtx.Seneitivity;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -85f, 85f);
        playerCtx.PlayerCamera.localRotation = Quaternion.Euler(pitch, 0, 0);
        playerCtx.CharacterController.transform.Rotate(Vector3.up * mouseX);
    }
}