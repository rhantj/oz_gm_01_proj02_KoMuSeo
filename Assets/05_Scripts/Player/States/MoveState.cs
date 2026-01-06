using StateController;
using UnityEngine;

public class MoveState : BaseState
{
    PlayerContext playerCtx;
    const float DEFAULT_MOVESPEED = 3f;
    private int hashMoveAnimation;

    public MoveState(PlayerController controller) : base(controller)
    {
        hashMoveAnimation = Animator.StringToHash("Velocity");
    }

    public override void OnEnterState()
    {
        playerCtx = Controller.playerCtx;
    }

    public override void OnUpdateState()
    {
        float currentSpeed = playerCtx.MoveSpeed * Time.deltaTime;
        var direction = Controller.inputDir;

        playerCtx.CharacterController.Move(direction * currentSpeed);
    }

    public override void OnExitState()
    {
        playerCtx.CharacterController.Move(Vector3.zero);
    }
}