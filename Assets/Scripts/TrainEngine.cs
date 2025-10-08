using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainEngine : InteractableObject
{
    [Header("�ӵ� ���� ����")]
    public float speedBoostPerCoal = 1f;
    public float speedBoostDuration = 5f;

    public override void Interact()
    {
        if (InteractionManager.Instance.TryUseItem(InteractionManager.ItemType.Coal))
        {
            GameManager.Instance.StartSpeedBoost(speedBoostPerCoal, speedBoostDuration);
            Debug.Log("��ź ����! �ӵ� ����!");
        }
        else
        {
            Debug.Log("��ź�� ��� ���� �ʽ��ϴ�!");
        }
    }
}
