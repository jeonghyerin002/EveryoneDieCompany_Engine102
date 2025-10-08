using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerData
{
    [Header("�÷��̾� ����")]
    public int playerLevel = 1;
    public int currentExp = 0;
    public int expToNextLevel = 100;
    public int gold = 0;

    [Header("���൵")]
    public int currentStageIndex = 0;
    public int maxUnlockedStage = 0;
    public List<int> completedStages = new List<int>();

    [Header("���� ����")]
    public List<string> ownedPartIDs = new List<string>();  //partData.partID
    public List<string> equippedPartIDs = new List<string>();

    [Header("���׷��̵� ����")]
    public int hpUpgradeLevel = 0;
    public int coalCapacityLevel = 0;
    public int cannonCapacityLevel = 0;
    public int speedUpgradeLevel = 0;
    public int repairEfficiencyLevel = 0;

    [Header("���")]
    public int totalGoldEarned = 0;
    public int totalEnemiesKilled = 0;
    public int totalStagesCompleted = 0;
    public float totalPlayTime = 0f;

}
