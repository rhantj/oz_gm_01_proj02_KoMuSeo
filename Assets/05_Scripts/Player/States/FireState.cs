using StateController;
using UnityEngine;

public class FireState : BaseState
{
    MainWeapon weapon;

    public FireState(PlayerController controller) : base(controller) { }

    public override void OnEnterState()
    {
        base.OnEnterState();
        weapon = Controller.GetComponentInChildren<MainWeapon>();
    }

    public override void OnUpdateState()
    {
        if (!Controller.isFiring)
        {
            Controller.playerCtx.ActionSM.ChangeState(StateName.ActionIdle);
        }
        weapon?.Fire();
    }

}