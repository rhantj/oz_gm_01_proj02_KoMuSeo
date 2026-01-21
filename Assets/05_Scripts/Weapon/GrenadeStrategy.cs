using UnityEngine;

public class GrenadeStrategy : IWeaponFireStrategy
{
    float throwPower;

    public GrenadeStrategy(float throwPower)
    {
        this.throwPower = throwPower;
    }

    public bool Fire(WeaponContext ctx)
    {
        if (Time.time < ctx.lastFireTime + ctx.fireRate) return false;
        ctx.lastFireTime = Time.time;

        // TODO : Change to ObjectPool
        var g = ObjectPoolManager.Instance.Spawn(PoolId.Grenade, ctx.muzzle.position, ctx.muzzle.rotation);

        var gd = g.GetComponent<GrenadeDamage>();
        gd.Init(ctx);

        var rb = g.GetComponent<Rigidbody>();
        rb.AddForce(ctx.muzzle.transform.forward * throwPower, ForceMode.Impulse);

        return true;
    }
}