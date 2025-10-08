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
                Debug.Log("¼®Åº È¹µæ! ³²Àº ¼®Åº :" + GameManager.Instance.trainData.currentCoal);
            }
            else
            {
                Debug.Log("¼®ÅºÀÌ ¾ø½À´Ï´Ù!");
            }
        }
    }
}
