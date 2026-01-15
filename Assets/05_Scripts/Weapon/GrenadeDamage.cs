using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeDamage : MonoBehaviour
{
    private float damage;
    private float maxRange;             // 30% damage
    
    private WeaponContext ctx;
    public LayerMask enemyLayer;

    [SerializeField] int maxTargets = 8;
    Collider[] targets;

    public void Init(WeaponContext ctx)
    {
        targets = new Collider[maxTargets];
        damage = ctx.damage;
        maxRange = ctx.maxRange;
        this.ctx = ctx;

        StopCoroutine(Co_Explosion());
        StartCoroutine(Co_Explosion());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }

    IEnumerator Co_Explosion()
    {
        yield return new WaitForSeconds(3f);

        int enemyCnt = Physics.OverlapSphereNonAlloc(transform.position, maxRange, targets, enemyLayer);
        Debug.Log($"A : {enemyCnt}");

        for (int i = 0; i < enemyCnt; ++i)
        {
            float dist = Vector3.Distance(transform.position, targets[i].transform.position);

            if (targets[i].gameObject.TryGetComponent<IDamageable>(out var dmg))
            {
                float t = Mathf.Clamp01(dist/ maxRange);
                float finalDmg = Mathf.Lerp(damage, 1f, t);
                finalDmg = Mathf.Max(finalDmg, 1f);

                DamageContext context = new()
                {
                    attacker = ctx.owner,
                    target = targets[i].gameObject,
                    hitPoint = targets[i].transform.position,
                    hitNormal = targets[i].transform.position.normalized,
                    damage = finalDmg,
                    damageType = DamageType.Explosion,
                    hitZone = ctx.dms.ResolveHitZone(targets[i])
                };

                DamageResult res = ctx.dms.Pipeline.Calculate(context);
                dmg.ApplyDamage(res);
            }
        }
    }
}