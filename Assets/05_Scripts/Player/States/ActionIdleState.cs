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
        if (Controller.fireInput.wasPressedThisFrame)
        {
            Controller.playerCtx.ActionSM.ChangeState(StateName.Fire);
        }

        if (Controller.isMelee)
        {
            Controller.playerCtx.ActionSM.ChangeState(StateName.Melee);
        }

        if (Controller.isReload) 
        {
            Controller.playerCtx.ActionSM.ChangeState(StateName.Reload);
        }
    }
}