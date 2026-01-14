using StateController;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Pool;

[RequireComponent(typeof(PlayerContext), typeof(PlayerInputActions))]
public class PlayerController : MonoBehaviour
{
    [Header("Player ref")]
    public PlayerContext playerCtx;
    public PlayerInputActions inputAction;
    public WeaponManager weaponManager;
    public FireInputContext fireInput;
    public StateName PrevMovementState { get; set; } = StateName.Move;

    public Vector3 inputDir;
    public Vector3 mouseDelta;
    float pitch = 0;
    public int weaponIdx = 0;
    public bool isSprinting = false;
    public bool ISGROUNDED;
    public bool isJump = false;
    public bool isCrouching = false;
    //public bool isFiring = false;
    public bool prevFirePressed;
    public bool isMelee;
    public bool isReload;

    Weapon currentWeapon;

    private void Awake()
    {
        playerCtx = GetComponent<PlayerContext>();
        inputAction = GetComponent<PlayerInputActions>();
        weaponManager = GetComponent<WeaponManager>();
        fireInput = new FireInputContext();

        StaticRegistry.Add(this);
    }

    private void Update()
    {
        OnMouseInput();
        ISGROUNDED = playerCtx.CharacterController.isGrounded;
    }

    private void OnEnable()
    {
        weaponManager.OnWeaponChanged += BindWeapon;
    }

    private void OnDisable()
    {
        weaponManager.OnWeaponChanged -= UnBindWeapon;
    }

    void BindWeapon(Weapon weapon)
    {
        weapon.OnAmmoEmpty += AmmoEmpty;
        currentWeapon = weapon;
    }

    void UnBindWeapon(Weapon weapon)
    {
        weapon.OnAmmoEmpty -= AmmoEmpty;
        currentWeapon = weapon;
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

    private void AmmoEmpty()
    {
        if (playerCtx.ActionSM.CurrentState is ReloadState)
            return;

        playerCtx.ActionSM.ChangeState(StateName.Reload);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputDir = new Vector3(input.x, 0, input.y);
    }

    public void OnMoveInputCanceled(InputAction.CallbackContext _)
    {
        inputDir = Vector3.zero;
    }

    public void OnSprintInput(InputAction.CallbackContext _)
    {
        if (inputDir != Vector3.zero)
            isSprinting = true;
    }

    public void OnSprintInputCanceled(InputAction.CallbackContext _)
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
        fireInput.isPressed = context.ReadValueAsButton();
    }

    public void OnFireInputCanceled(InputAction.CallbackContext context)
    {
        fireInput.isPressed = context.ReadValueAsButton();
    }

    public void OnModeInput(InputAction.CallbackContext _)
    {
        weaponManager.GetCurrentWeapon().NextFireMode();
    }

    public void OnMainWeaponInput(InputAction.CallbackContext _)
    {
        if (isReload) return;
        weaponManager.Equip(0);
    }
    public void OnSubWeaponInput(InputAction.CallbackContext _)
    {
        if (isReload) return;
        weaponManager.Equip(1);
    }

    public void OnGrenadeInput(InputAction.CallbackContext _)
    {
        if (isReload) return;
        weaponManager.Equip(2);
    }

    public void OnMeleeInput(InputAction.CallbackContext context)
    {
        isMelee = context.ReadValueAsButton();
    }

    public void OnMeleeInputCanceled(InputAction.CallbackContext context)
    {
        isMelee = context.ReadValueAsButton();
    }

    public void OnReloadInput(InputAction.CallbackContext context)
    {
        if (isReload && currentWeapon.CurrentMag == currentWeapon.MaxMag) 
            return;
        isReload = context.ReadValueAsButton();
    }
}