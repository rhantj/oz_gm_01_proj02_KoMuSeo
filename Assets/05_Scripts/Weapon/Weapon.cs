using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum FireMode
{
    Single,
    SemiAuto,
    Auto
}

public abstract class Weapon : MonoBehaviour
{
    protected WeaponContext context;
    protected IWeaponFireStrategy fireStrategy;
    protected Dictionary<FireMode, IFireModeStrategy> fireModes = new();
    [SerializeField] protected FireMode currentMode;
    FireMode[] modes;

    [Header("References"), Tooltip("탄환과 이펙트가 나오는 위치")]
    public Transform muzzle;
    public Transform muzzleVFX;

    [Header("Hit-scan"), Tooltip("히트 스캔 방식에 필요한 변수")]
    public float hitscanRange = 30f;
    public int hitDamage = 30;

    [Header("Ballistic"), Tooltip("탄도학 계산에 필요한 변수")]
    public float bulletSpeed = 800f;
    public float fireRate = 0.1f;
    public float maxRange = 150f;

    [Header("Spread"), Tooltip("탄환 퍼짐 정도")]
    public float spreadAngle = 0.6f;

    [Header("Mag"), Tooltip("장탄 수 제한")]
    public int maxMag;
    public int currentMag;

    public event Action OnWeaponFire;
    public event Action OnReload;
    public event Action OnAmmoEmpty;

    protected virtual void Awake()
    {
        context = new();
        fireStrategy = new HybridFireStrategy();
        modes = Enum.GetValues(typeof(FireMode)).Cast<FireMode>().ToArray();

        currentMag = maxMag;
    }

    protected virtual void Start()
    {
        context.dms = StaticRegistry.Find<DamageSystem>();
    }

    public void SetFireMode(FireMode mode)
    {
        if (!fireModes.ContainsKey(mode)) return;
        currentMode = mode;
    }

    public void NextFireMode()
    {
        int startIdx = Array.IndexOf(modes, currentMode);

        for (int i = 1; i <= modes.Length; ++i)
        {
            FireMode next = modes[(startIdx + i) % modes.Length];
            if (fireModes.ContainsKey(next))
            {
                currentMode = next;
                return;
            }
        }
    }

    public virtual void Fire(FireInputContext input)
    {
        fireModes[currentMode].Tick(this, context, input);
    }

    public virtual void DoFire(WeaponContext context)
    {
        if (currentMag <= 0)
        {
            AmmoEmptyInvok();
            return;
        }

        if (!fireStrategy.Fire(context)) return;

        currentMag--;
        OnWeaponFire?.Invoke();

        if (currentMag <= 0)
        {
            AmmoEmptyInvok();
        }
    }

    public void ReloadInvoke()
    {
        OnReload?.Invoke();
    }

    void AmmoEmptyInvok()
    {
        OnAmmoEmpty?.Invoke();
    }
}