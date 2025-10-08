using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerData
{
    [Header("플레이어 정보")]
    public int playerLevel = 1;
    public int currentExp = 0;
    public int expToNextLevel = 100;
    public int gold = 0;

    [Header("진행도")]
    public int currentStageIndex = 0;
    public int maxUnlockedStage = 0;
    public List<int> completedStages = new List<int>();

    [Header("소유 파츠")]
    public List<string> ownedPartIDs = new List<string>();  //partData.partID
    public List<string> equippedPartIDs = new List<string>();

    [Header("업그레이드 레벨")]
    public int hpUpgradeLevel = 0;
    public int coalCapacityLevel = 0;
    public int cannonCapacityLevel = 0;
    public int speedUpgradeLevel = 0;
    public int repairEfficiencyLevel = 0;

    [Header("통계")]
    public int totalGoldEarned = 0;
    public int totalEnemiesKilled = 0;
    public int totalStagesCompleted = 0;
    public float totalPlayTime = 0f;

}
