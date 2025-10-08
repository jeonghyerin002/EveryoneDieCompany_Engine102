using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : InteractableObject
{
    [Header("���� ����")]
    public int repairAmount = 20;
    public int repairCost = 10;

    public override void Interact()
    {
        TrainData train = GameManager.Instance.trainData;
        PlayerData player = GameManager.Instance.playerData;

        if(train.currentHP >= train.maxHP)
        {
            Debug.Log("�̹� ü���� ���� á���ϴ�!");
            return;
        }
        if (player.gold < repairCost)
        {
            Debug.Log("��尡 �����մϴ�! �ʿ� :" + repairCost + "G");
            return;
        }
        player.gold = repairCost;
        train.currentHP = repairAmount;

        if (train.currentHP > train.maxHP)
            train.currentHP = train.maxHP;

        Debug.Log("���� �Ϸ�! ���� HP :" + train.currentHP);
    }
}
