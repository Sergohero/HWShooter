using System;
using UnityEngine;

namespace HWShoter
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _weapon.Fire();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _weapon.Recharge();
            }
            
        }
    }
}