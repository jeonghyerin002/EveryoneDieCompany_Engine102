using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartInventory : MonoBehaviour
{
    private static PlayerPartInventory instance;
    public static PlayerPartInventory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerPartInventory>();
                if (instance == null)
                {
                    GameObject go = new GameObject("PlayerPartInventory");
                    instance = go.AddComponent<PlayerPartInventory>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    [Header("보유 파트")]
    public List<PartData> ownedParts = new List<PartData>();

    [Header("현재 스탯")]
    public int totalHP = 100;           // 기본 HP
    public int totalCoalCapacity = 50;  // 기본 석탄 용량
    public int totalAmmoCapacity = 30;  // 기본 탄약 용량
    public float totalSpeed = 10f;      // 기본 속도
    public float totalDamage = 10f;     // 기본 데미지
    public float totalFireRate = 1f;    // 기본 연사력

    public int partHPBonusSum;
    public int partCoalBonusSum;
    public int partAmmoBonusSum;
    public float partSpeedBonusSum;
    public float partDamageBonusSum;
    public float partFireRateBonusSum;

    private PlayerData _fallbackPD; // GameManager 없는 씬(상점) 대비용

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RecalcPartBonuses()
    {
        partHPBonusSum = 0;
        partCoalBonusSum = 0;
        partAmmoBonusSum = 0;
        partSpeedBonusSum = 0f;
        partDamageBonusSum = 0f;
        partFireRateBonusSum = 0f;

        foreach (PartData part in ownedParts)
        {
            if (part == null) continue;
            partHPBonusSum += part.hpBonus;
            partCoalBonusSum += part.coalCapacityBonus;
            partAmmoBonusSum += part.cannonCapacityBonus;
            partSpeedBonusSum += part.speedBonus;
            partDamageBonusSum += part.damageBonus;
            partFireRateBonusSum += part.fireRateBonus;
        }
    }

    // 파트 추가 시 보너스 재계산만 호출
    public void AddPart(PartData newPart)
    {
        if (newPart == null) return;
        ownedParts.Add(newPart);
        RecalcPartBonuses();
        Debug.Log("파트 획득: " + newPart.partName);
    }

    // 전체 스탯 계산
    public void CalculateTotalStats()
    {
        // 기본값으로 리셋
        totalHP = 100;
        totalCoalCapacity = 50;
        totalAmmoCapacity = 30;
        totalSpeed = 10f;
        totalDamage = 10f;
        totalFireRate = 1f;

        // 모든 보유 파트의 보너스 추가
        foreach (PartData part in ownedParts)
        {
            if (part != null)
                ApplyPartBonus(part);
        }

        Debug.Log("스탯 재계산 - HP: " + totalHP + ", 속도: " + totalSpeed);
    }

    // 파트 보너스 적용
    void ApplyPartBonus(PartData part)
    {
        totalHP += part.hpBonus;
        totalCoalCapacity += part.coalCapacityBonus;
        totalAmmoCapacity += part.cannonCapacityBonus;
        totalSpeed += part.speedBonus;
        totalDamage += part.damageBonus;
        totalFireRate += part.fireRateBonus;
    }

    public PlayerData GetPD()
    {
        if (GameManager.Instance != null && GameManager.Instance.playerData != null)
            return GameManager.Instance.playerData;

        // 상점 씬처럼 GameManager가 없을 때 임시 PlayerData 사용
        if (_fallbackPD == null) _fallbackPD = new PlayerData();
        return _fallbackPD;
    }

    public TrainData GetTD()
    {
        if (GameManager.Instance != null)
            return GameManager.Instance.trainData;

        // TrainData는 주로 게임 씬에서 쓰이니, 없으면 null 리턴 (UI만 쓰면 null 체크)
        return null;
    }
}