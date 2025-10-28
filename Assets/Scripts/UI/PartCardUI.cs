using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartCardUI : MonoBehaviour
{
    [Header("UI 요소")]
    public Text partNameText;
    public Text partDescriptionText;
    public Image partIconImage;
    public Text partLevelText;

    [Header("스탯 표시")]
    public Text statText; // 주요 스탯 1개만 표시

    private PartData currentPart;

    // 파트 데이터로 카드 UI 설정
    public void SetupCard(PartData part)
    {
        currentPart = part;

        // 기본 정보 표시
        if (partNameText != null)
            partNameText.text = part.partName;

        if (partDescriptionText != null)
            partDescriptionText.text = part.description;

        if (partIconImage != null && part.partIcon != null)
            partIconImage.sprite = part.partIcon;

        if (partLevelText != null)
            partLevelText.text = "Lv." + part.partLevel;

        // 스탯 표시
        ShowStats(part);
    }

    // 스탯 표시
    void ShowStats(PartData part)
    {
        if (statText == null) return;

        // 가장 높은 보너스 값 1개만 표시
        string statString = "";

        if (part.hpBonus > 0)
            statString = "HP +" + part.hpBonus;
        else if (part.coalCapacityBonus > 0)
            statString = "석탄 +" + part.coalCapacityBonus;
        else if (part.cannonCapacityBonus > 0)
            statString = "탄약 +" + part.cannonCapacityBonus;
        else if (part.speedBonus > 0)
            statString = "속도 +" + part.speedBonus.ToString("F1");
        else if (part.damageBonus > 0)
            statString = "공격력 +" + part.damageBonus.ToString("F1");
        else if (part.fireRateBonus > 0)
            statString = "연사력 +" + part.fireRateBonus.ToString("F1");

        statText.text = statString;
    }

    public PartData GetPartData()
    {
        return currentPart;
    }
}