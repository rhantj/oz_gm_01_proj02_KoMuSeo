using UnityEngine;

public class GrenadeStrategy : IWeaponFireStrategy
{
    GameObject projectilePF;
    float throwPower;

    public GrenadeStrategy(GameObject projectilePF, float throwPower)
    {
        this.projectilePF = projectilePF;
        this.throwPower = throwPower;
    }

    public bool Fire(WeaponContext ctx)
    {
        if (Time.time < ctx.lastFireTime + ctx.fireRate) return false;
        ctx.lastFireTime = Time.time;

        // TODO : Change to ObjectPool
        var g = GameObject.Instantiate(projectilePF, ctx.muzzle.transform.position, Quaternion.identity);

        var gd = g.GetComponent<GrenadeDamage>();
        gd.Init(ctx);

        var rb = g.GetComponent<Rigidbody>();
        rb.AddForce(ctx.muzzle.transform.forward * throwPower, ForceMode.Impulse);

        return true;
    }
}