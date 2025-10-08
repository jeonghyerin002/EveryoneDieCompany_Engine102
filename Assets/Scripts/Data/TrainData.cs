using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

[CreateAssetMenu (fileName = "TrainData", menuName = "Game/Train Data")]
public class TrainData : ScriptableObject
{
    [Header("열차 기본 스탯")]
    public int baseMaxHP = 100;
    public float baseSpeed = 1f;
    public int baseMaxCoal = 10;
    public int baseMaxCannonBalls = 20;

    [Header("석탄 설정")]
    public float speedPerCoal = 0.5f;        //석탄당 속조 증가
    public float coalConsumptionRate = 0.1f; //초당 석탄 소모량

    [Header("수리 설정")]
    public int repairCostPerHP = 10;
    public float repairTime = 2f;

    [Header("업그레이드당 증가량")]
    public int hpPerUpgrade = 20;
    public int coalPerUpgrade = 2;
    public int cannonPerUpgrade = 5;
    public float speedPerUpgrade = 0.2f;

    //런타임 값 (게임 중 변하는 값)
    [HideInInspector] public int currentHP;
    [HideInInspector] public int currentCoal;
    [HideInInspector] public int currentCannonBalls;
    [HideInInspector] public int maxHP;
    [HideInInspector] public int maxCoal;
    [HideInInspector] public int maxCannonBalls;
}
