using StateController;
using UnityEngine;

public class FireState : BaseState
{
    Weapon currentWeapon;

    public FireState(PlayerController controller) : base(controller) { }

    public override void OnEnterState()
    {
        base.OnEnterState();
        currentWeapon = Controller.weapons.GetCurrentWeapon();
    }

    public override void OnUpdateState()
    {
        if (!Controller.fireInput.isPressed)
        {
            Controller.playerCtx.ActionSM.ChangeState(StateName.ActionIdle);
            return;
        }

        currentWeapon.Fire(Controller.fireInput);
    }

}