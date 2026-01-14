using StateController;
using UnityEngine;

public class ThrowState : BaseState
{
    public ThrowState(PlayerController controller) : base(controller) { }

    const float THROW_DELAY = 0.3f;
    const float THROW_END = 0.6f;
    float timer;

    bool thrown;
    Weapon currentWeapon;

    public override void OnEnterState()
    {
        timer = 0f;
        thrown = false;
        currentWeapon = Controller.weaponManager.GetCurrentWeapon();
    }

    public override void OnUpdateState()
    {
        timer += Time.deltaTime;

        if (!thrown && timer >= THROW_DELAY)
        {
            thrown = true;
            currentWeapon.Fire(new FireInputContext
            {
                isPressed = true,
                wasPressedThisFrame = true
            });
        }

        if(timer >= THROW_END)
        {
            Controller.playerCtx.ActionSM.ChangeState(StateName.ActionIdle);
        }
    }
}