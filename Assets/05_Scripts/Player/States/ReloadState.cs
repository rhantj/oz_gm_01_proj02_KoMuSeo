using StateController;
using UnityEngine;

public class ReloadState : BaseState
{
    float timer;
    bool takeoffMagazine;
    const float TAKEOFF_MAGAZINE_TIME = 0.2f;
    const float RELOADING_TIME = 0.8f;

    Weapon currentWeapon;

    public ReloadState(PlayerController controller) : base(controller) { }

    public override void OnEnterState()
    {
        base.OnEnterState();
        timer = 0;
        takeoffMagazine = false;

        currentWeapon = Controller.weapons.GetCurrentWeapon();
        currentWeapon.ReloadInvoke();
    }

    public override void OnUpdateState()
    {
        timer += Time.deltaTime;

        if(timer >= TAKEOFF_MAGAZINE_TIME && !takeoffMagazine)
        {
            takeoffMagazine = true;
        }

        if (timer >= RELOADING_TIME)
        {
            Controller.playerCtx.ActionSM.ChangeState(StateName.ActionIdle);
        }
    }

    public override void OnExitState()
    {
        currentWeapon.currentMag = currentWeapon.maxMag;
        Controller.isReload = false;
    }
}