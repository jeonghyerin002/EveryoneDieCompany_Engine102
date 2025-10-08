using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("�������� ����")]
    public int stageNumber = 1;
    public string stageName = "1�� �뼱";
    public Sprite stageIcon;

    [Header("��ǥ")]
    public float stageDistance = 100f; //���� �Ÿ�
    public int requiredCoal = 5;       //���� ��ź ��

    [Header("����")]
    public int baseExpReward = 50;
    public int baseGoldReward = 100;
}
