using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSlot : InteractableObject
{
    [Header("��ž ����")]
    public TurretController turret;
    public int bulletsPerCannonball = 10;

    public override void Interact()
    {
        if (InteractionManager.Instance.TryUseItem(InteractionManager.ItemType.Cannonball))
        {
            if (turret != null)
            {
                turret.AddBullets(bulletsPerCannonball);
                Debug.Log("��ź ����! ��ž ź�� +" + bulletsPerCannonball);
            }
        }
        else
        {
            Debug.Log("��ź�� ��� ���� �ʽ��ϴ�!");
        }
    }
}
