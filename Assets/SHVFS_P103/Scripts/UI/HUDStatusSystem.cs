using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SHVFS_P103.Scripts.UI
{
    public class HUDStatusSystem : Singleton<HUDStatusSystem>
    {
        public TextMeshProUGUI AmmoCount;
        public Image HealthBar;
        public TextMeshProUGUI RockCount;
        public PlayerStatusComponent PlayerStatusComponent;

        private void Start()
        {
            UpdateAmmoCount();
            UpdateHealthPoint();
            UpdateRockCount();
        }
        
        public void UpdateAmmoCount()
        {
            if (!PlayerStatusComponent || !AmmoCount) return;

            if (PlayerStatusComponent.gameObject.GetComponentInChildren<IWeapon>() == null) return;
            
            var curAmmoCount = PlayerStatusComponent.gameObject.GetComponentInChildren<IWeapon>().GetCurAmmoCount();

            AmmoCount.text = curAmmoCount + " / " + PlayerStatusComponent.AmmoInventory;
        }

        public void UpdateHealthPoint()
        {
            if (!PlayerStatusComponent || !HealthBar) return;

            if (PlayerStatusComponent.MaxHealthPoint == 0) return;

            HealthBar.fillAmount = (float)PlayerStatusComponent.HealthPoint / PlayerStatusComponent.MaxHealthPoint;
        }

        public void UpdateRockCount()
        {
            if (!PlayerStatusComponent || !RockCount) return;

            RockCount.text = "" + PlayerStatusComponent.RockInventory;
        }
    }
}
