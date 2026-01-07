using StateController;
using UnityEngine;

public class CrouchState : BasePlayerState
{
    public CrouchState(PlayerController controller) : base(controller) { }
    Vector3 camPos;
    float speed;

    public override void OnEnterState()
    {
        base.OnEnterState();

        camPos = playerCtx.PlayerCamera.localPosition;
        camPos.y *= 0.3f;
        speed = playerCtx.MoveSpeed;
        speed *= 0.3f;

        playerCtx.PlayerCamera.localPosition -= camPos;
        playerCtx.MoveSpeed -= speed;
    }

    public override void OnUpdateState()
    {
        if (!Controller.isCrouching)
        {
            var state = Controller.inputDir.sqrMagnitude > 0.01f ? StateName.Move : StateName.Idle;
            playerCtx.MovementSM.ChangeState(state);
        }

        if (Controller.isJump)
        {
            var state = Controller.inputDir.sqrMagnitude > 0.01f ? StateName.Move : StateName.Idle;
            playerCtx.MovementSM.ChangeState(state);
        }

        ApplyGravity();
        CommonMovement();
    }

    public override void OnExitState()
    {
        playerCtx.PlayerCamera.localPosition += camPos;
        playerCtx.MoveSpeed += speed;
        Controller.isJump = false;
    }
}