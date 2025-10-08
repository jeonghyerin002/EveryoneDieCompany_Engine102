using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

[CreateAssetMenu (fileName = "TrainData", menuName = "Game/Train Data")]
public class TrainData : ScriptableObject
{
    [Header("���� �⺻ ����")]
    public int baseMaxHP = 100;
    public float baseSpeed = 1f;
    public int baseMaxCoal = 10;
    public int baseMaxCannonBalls = 20;

    [Header("��ź ����")]
    public float speedPerCoal = 0.5f;        //��ź�� ���� ����
    public float coalConsumptionRate = 0.1f; //�ʴ� ��ź �Ҹ�

    [Header("���� ����")]
    public int repairCostPerHP = 10;
    public float repairTime = 2f;

    [Header("���׷��̵�� ������")]
    public int hpPerUpgrade = 20;
    public int coalPerUpgrade = 2;
    public int cannonPerUpgrade = 5;
    public float speedPerUpgrade = 0.2f;

    //��Ÿ�� �� (���� �� ���ϴ� ��)
    [HideInInspector] public int currentHP;
    [HideInInspector] public int currentCoal;
    [HideInInspector] public int currentCannonBalls;
    [HideInInspector] public int maxHP;
    [HideInInspector] public int maxCoal;
    [HideInInspector] public int maxCannonBalls;
}
