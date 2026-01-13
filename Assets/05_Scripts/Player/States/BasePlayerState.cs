using StateController;
using UnityEngine;
public abstract class BasePlayerState : BaseState
{
    protected PlayerContext playerCtx;
    protected float yVelocity;
    protected const float gravity = -20f;

    public BasePlayerState(PlayerController controller) : base(controller) { }

    public override void OnEnterState()
    {
        playerCtx = Controller.playerCtx;
    }

    public override void OnUpdateState()
    {
        CommonMovement();
    }

    public void ApplyGravity()
    {
        if (playerCtx.CharacterController.isGrounded && yVelocity < 0f)
            yVelocity = -1f;

        yVelocity += gravity * Time.deltaTime;
    }

    public void CommonMovement()
    {
        ApplyGravity();
        float currentSpeed = playerCtx.MoveSpeed * Time.deltaTime;
        Vector3 moveDir = Controller.transform.right * Controller.inputDir.x + 
            Controller.transform.forward * Controller.inputDir.z;

        
        Vector3 velocity = moveDir * currentSpeed;
        velocity.y = yVelocity * Time.deltaTime;

        playerCtx.CharacterController.Move(velocity);
    }
}