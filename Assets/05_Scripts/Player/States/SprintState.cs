public class SprintState : BasePlayerState
{
    float speed;

    public SprintState(PlayerController controller) : base(controller) { }
    public override void OnEnterState()
    {
        base.OnEnterState();

        speed = playerCtx.MoveSpeed;
        speed *= 0.5f;

        playerCtx.MoveSpeed += speed;
    }

    public override void OnUpdateState()
    {
        if (Controller.isJump && playerCtx.CharacterController.isGrounded)
        {
            Controller.isJump = false;
            playerCtx.MovementSM.ChangeState(StateController.StateName.Jump);
            return;
        }

        if (!Controller.isSprinting || Controller.isCrouching || Controller.inputDir.sqrMagnitude <= 0.01f)
        {
            playerCtx.MovementSM.ChangeState(StateController.StateName.Move);
            return;
        }

        ApplyGravity();
        CommonMovement();
    }

    public override void OnExitState()
    {
        playerCtx.MoveSpeed -= speed;
    }
}