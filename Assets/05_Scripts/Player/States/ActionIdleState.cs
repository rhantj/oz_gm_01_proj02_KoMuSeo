using StateController;

public class ActionIdleState : BaseState
{
    public ActionIdleState(PlayerController controller) : base(controller) { }

    public override void OnEnterState()
    {
        base.OnEnterState();
    }

    public override void OnUpdateState()
    {
        if (Controller.isFiring)
        {
            Controller.playerCtx.ActionSM.ChangeState(StateName.Fire);
        } 
    }
}