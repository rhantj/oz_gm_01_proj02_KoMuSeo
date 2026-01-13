using TMPro;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI ammoText;

    PlayerController Controller;

    private void Start()
    {
        Controller = StaticRegistry.Find<PlayerController>();
    }

    public void UpdateAmmoUI()
    {
        int maxAmmo = Controller.weapons.currentWeapon.maxMag;
        int curAmmo = Controller.weapons.currentWeapon.currentMag;

        ammoText.text = $"{curAmmo} / {maxAmmo}";
    }
}
