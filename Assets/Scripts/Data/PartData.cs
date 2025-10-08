using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartData", menuName = "Game/Part Data")]
public class PartData : ScriptableObject
{
    [Header("파트 정보")]
    public string partID; //고유 ID
    public string partName = "강화장갑";
    public Sprite partIcon;
    [TextArea] public string description = "열차 최대 HP +20";
    public PartType partType;
    public int partLevel = 1;

    [Header("효과")]
    public int hpBonus = 0;
    public int coalCapacityBonus = 0;
    public int cannonCapacityBonus = 0;
    public float speedBonus = 0f;
    public float damageBonus = 0f;
    public float fireRateBonus = 0f;

    [Header("가격")]
    public int goldCost = 100;

    public enum PartType
    {
        Armor,      //장갑 (Hp)
        Engine,     //엔진 (속도)
        CoalStorage,//석탄 저장고
        AmmoStorage //탄약 저장고
    }


}
