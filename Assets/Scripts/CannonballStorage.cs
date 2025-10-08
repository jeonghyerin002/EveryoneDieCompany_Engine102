using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractionManager;

public class CannonballStorage : InteractableObject
{
    public override void Interact()
    {
        if (!InteractionManager.Instance.IsHoldingItem())
        {
            if(GameManager.Instance.trainData.currentCannonBalls > 0)
            {
                InteractionManager.Instance.PickupItem(ItemType.Cannonball);
                GameManager.Instance.trainData.currentCannonBalls--;
                Debug.Log("��ź ȹ��! ���� ��ź :" + GameManager.Instance.trainData.currentCannonBalls);
            }
            else
            {
                Debug.Log("��ź�� �����ϴ�!");
            }
        }
    }

}
