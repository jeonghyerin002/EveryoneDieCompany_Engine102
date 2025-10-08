using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : InteractableObject
{
    [Header("수리 설정")]
    public int repairAmount = 20;
    public int repairCost = 10;

    public override void Interact()
    {
        TrainData train = GameManager.Instance.trainData;
        PlayerData player = GameManager.Instance.playerData;

        if(train.currentHP >= train.maxHP)
        {
            Debug.Log("이미 체력이 가득 찼습니다!");
            return;
        }
        if (player.gold < repairCost)
        {
            Debug.Log("골드가 부족합니다! 필요 :" + repairCost + "G");
            return;
        }
        player.gold = repairCost;
        train.currentHP = repairAmount;

        if (train.currentHP > train.maxHP)
            train.currentHP = train.maxHP;

        Debug.Log("수리 완료! 현재 HP :" + train.currentHP);
    }
}
