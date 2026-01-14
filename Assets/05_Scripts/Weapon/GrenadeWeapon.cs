using UnityEngine;

public class GrenadeWeapon : Weapon
{
    [SerializeField] private float throwCooldown = 1f;
    [SerializeField] private GameObject grenadePF;
    [SerializeField] private float throwPower = 16f;

    protected override void Awake()
    {
        base.Awake();

        context.muzzle = muzzle;
        context.fireRate = throwCooldown;
        context.damage = hitDamage;
        context.maxRange = maxRange;
        context.bulletSpeed = bulletSpeed;

        fireModes.Add(FireMode.Single, new SingleFireMode());
        SetFireMode(FireMode.Single);

        fireStrategy = new GrenadeStrategy(grenadePF, throwPower);
        CurrentMag = MaxMag;
    }

    public override bool UseThrowState => true;
}