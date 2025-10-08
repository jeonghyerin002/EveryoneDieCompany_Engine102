using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractionManager;

public class CoalStorage : InteractableObject
{
    public override void Interact()
    {
        if (!InteractionManager.Instance.IsHoldingItem())
        {
            if (GameManager.Instance.trainData.currentCoal > 0)
            {
                InteractionManager.Instance.PickupItem(ItemType.Coal);
                GameManager.Instance.trainData.currentCoal--;
                Debug.Log("��ź ȹ��! ���� ��ź :" + GameManager.Instance.trainData.currentCoal);
            }
            else
            {
                Debug.Log("��ź�� �����ϴ�!");
            }
        }
    }
}
