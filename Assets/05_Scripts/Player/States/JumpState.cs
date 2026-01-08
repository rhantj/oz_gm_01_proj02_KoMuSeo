public class JumpState : BasePlayerState
{
    public JumpState(PlayerController controller) : base(controller) { }

    public override void OnEnterState()
    {
        base.OnEnterState();
        yVelocity = playerCtx.JumpForce;
    }

    public override void OnUpdateState()
    {
        ApplyGravity();
        CommonMovement();

        if (playerCtx.CharacterController.isGrounded && yVelocity <= 0f)
        {
            playerCtx.MovementSM.ChangeState(Controller.PrevMovementState);
        }
    }

    public override void OnExitState()
    {
        Controller.isJump = false;
    }
}