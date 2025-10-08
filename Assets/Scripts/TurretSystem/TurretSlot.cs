using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSlot : InteractableObject
{
    [Header("포탑 설정")]
    public TurretController turret;
    public int bulletsPerCannonball = 10;

    public override void Interact()
    {
        if (InteractionManager.Instance.TryUseItem(InteractionManager.ItemType.Cannonball))
        {
            if (turret != null)
            {
                turret.AddBullets(bulletsPerCannonball);
                Debug.Log("포탄 장전! 포탑 탄약 +" + bulletsPerCannonball);
            }
        }
        else
        {
            Debug.Log("포탄을 들고 있지 않습니다!");
        }
    }
}
