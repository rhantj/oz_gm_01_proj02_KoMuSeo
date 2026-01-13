using StateController;
using UnityEngine;
using System.Collections.Generic;

public class MeleeState : BaseState
{
    float timer;
    bool attacked;

    float angle = 60;
    float distance = 5f;
    int rayCount = 10;
    LayerMask hitLayer = LayerMask.GetMask("Enemy");

    const float MELEE_DURATION = 0.8f;
    const float HIT_TIME = 0.1f;
    HashSet<GameObject> targets = new();

    public MeleeState(PlayerController controller) : base(controller) { }

    public override void OnEnterState()
    {
        base.OnEnterState();

        timer = 0f;
        attacked = false;

        //Controller.playerCtx.Anim.Play("Melee");
    }

    public override void OnUpdateState()
    {
        timer += Time.deltaTime;

        if (!attacked && timer >= HIT_TIME)
        {
            MeleeHit();
            attacked = true;
        }

        if (timer >= MELEE_DURATION)
        {
            Controller.playerCtx.ActionSM.ChangeState(StateName.ActionIdle);
        }

    }

    void MeleeHit()
    {
        targets.Clear();
        float half = angle * 0.5f;

        for (int i = 0; i < rayCount; ++i)
        {
            float currentAngle = -half + (angle / rayCount) * i;
            var cam = Camera.main.transform.forward;
            var dir = Quaternion.AngleAxis(currentAngle, Vector3.up) * cam;

            if (Physics.Raycast(Controller.transform.position, dir, out var hit, distance, hitLayer))
            {
                Debug.DrawLine(Controller.transform.position, hit.point, Color.green);
                targets.Add(hit.collider.gameObject);
            }
            else
            {
                Debug.DrawRay(Controller.transform.position, dir * distance, Color.red);
            }
        }

        foreach(var t in targets)
        {
            if(t.TryGetComponent<Target>(out var target))
            {
                target.hp -= 200;
            }
        }
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }
}