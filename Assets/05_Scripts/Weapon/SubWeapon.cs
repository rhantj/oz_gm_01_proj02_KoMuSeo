using UnityEngine;

public class SubWeapon : Weapon
{
    protected override void Awake()
    {
        base.Awake();

        context.muzzle = muzzle;
        context.fireRate = fireRate;
        context.damage = hitDamage;
        context.hitscanRange = hitscanRange;
        context.bulletSpeed = bulletSpeed;
        context.maxRange = maxRange;
        context.spreadAngle = spreadAngle;

        currentMag = maxMag;

        fireModes.Add(FireMode.Single, new SingleFireMode());
        SetFireMode(FireMode.Single);
    }
}