using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartData", menuName = "Game/Part Data")]
public class PartData : ScriptableObject
{
    [Header("��Ʈ ����")]
    public string partID; //���� ID
    public string partName = "��ȭ�尩";
    public Sprite partIcon;
    [TextArea] public string description = "���� �ִ� HP +20";
    public PartType partType;
    public int partLevel = 1;

    [Header("ȿ��")]
    public int hpBonus = 0;
    public int coalCapacityBonus = 0;
    public int cannonCapacityBonus = 0;
    public float speedBonus = 0f;
    public float damageBonus = 0f;
    public float fireRateBonus = 0f;

    [Header("����")]
    public int goldCost = 100;

    public enum PartType
    {
        Armor,      //�尩 (Hp)
        Engine,     //���� (�ӵ�)
        CoalStorage,//��ź �����
        AmmoStorage //ź�� �����
    }


}
