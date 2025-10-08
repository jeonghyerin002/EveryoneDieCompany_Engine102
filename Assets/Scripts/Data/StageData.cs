using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("스테이지 정보")]
    public int stageNumber = 1;
    public string stageName = "1번 노선";
    public Sprite stageIcon;

    [Header("목표")]
    public float stageDistance = 100f; //완주 거리
    public int requiredCoal = 5;       //권장 석탄 수

    [Header("보상")]
    public int baseExpReward = 50;
    public int baseGoldReward = 100;
}
