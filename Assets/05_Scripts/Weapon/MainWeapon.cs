using System;
using UnityEngine;

public class MainWeapon : Weapon
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
        fireModes.Add(FireMode.SemiAuto, new SemiAutoFireMode());
        fireModes.Add(FireMode.Auto, new FullAutoFireMode());

        SetFireMode(FireMode.Auto);
    }
}
