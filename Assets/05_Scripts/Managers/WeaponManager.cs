using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class WeaponManager : MonoBehaviour
{
    [Header("Player weapon")]
    public Weapon[] weapons;
    public Weapon currentWeapon;

    public event Action<Weapon> OnWeaponChanged;

    private void Awake()
    {
        for (int i = 0; i < weapons.Length && weapons.Length > 0; ++i)
        {
            weapons[i].gameObject.SetActive(false);
        }

        Equip(0);
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void SetCurrentWeapon(int idx)
    {
        currentWeapon = weapons[idx];
    }

    public void Equip(int idx)
    {
        if (idx < 0 || idx >= weapons.Length) return;

        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }

        currentWeapon = weapons[idx];
        currentWeapon.gameObject.SetActive(true);

        OnWeaponChanged?.Invoke(currentWeapon);
    }
}