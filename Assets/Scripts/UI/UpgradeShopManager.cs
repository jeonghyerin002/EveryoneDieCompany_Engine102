
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeShopManager : MonoBehaviour
{
    [Header("UI (골드/비용/레벨)")]
    public Text goldText;

    public Text hpCostText;
    public Text coalCostText;
    public Text cannonCostText;
    public Text speedCostText;

    public Text hpLevelText;
    public Text coalLevelText;
    public Text cannonLevelText;
    public Text speedLevelText;

    [Header("구매 버튼")]
    public Button buyHpButton;
    public Button buyCoalButton;
    public Button buyCannonButton;
    public Button buySpeedButton;

    [Header("업그레이드 비용 설정")]
    public int baseHpCost = 100;
    public int baseCoalCost = 80;
    public int baseCannonCost = 90;
    public int baseSpeedCost = 120;

    [Tooltip("레벨이 1 오를 때마다 비용에 곱해질 성장 계수(>1 권장)")]
    public float growth = 1.35f;

    void Awake()
    {
        BindButtons(); // 리스너 연결
    }

    void OnEnable()
    {
        RefreshUI();  // 화면 들어올 때 UI 갱신
    }

    void Start()
    {
        RefreshUI();  // 초기 1회 갱신
    }

    // ----------------------
    // 버튼 리스너 바인딩
    // ----------------------
    void BindButtons()
    {
        if (buyHpButton)
        {
            buyHpButton.onClick.RemoveAllListeners();
            buyHpButton.onClick.AddListener(BuyHP);
        }
        if (buyCoalButton)
        {
            buyCoalButton.onClick.RemoveAllListeners();
            buyCoalButton.onClick.AddListener(BuyCoal);
        }
        if (buyCannonButton)
        {
            buyCannonButton.onClick.RemoveAllListeners();
            buyCannonButton.onClick.AddListener(BuyCannon);
        }
        if (buySpeedButton)
        {
            buySpeedButton.onClick.RemoveAllListeners();
            buySpeedButton.onClick.AddListener(BuySpeed);
        }
    }

    // ----------------------
    // 구매 함수
    // ----------------------
    public void BuyHP()
    {
        var pd = PlayerPartInventory.Instance.GetPD();
        int cost = GetCost(baseHpCost, pd.hpUpgradeLevel);
        if (TrySpend(cost))
        {
            pd.hpUpgradeLevel++;
            RefreshUI();
        }
        else ShakeCost(hpCostText);
    }

    public void BuyCoal()
    {
        var pd = PlayerPartInventory.Instance.GetPD();
        int cost = GetCost(baseCoalCost, pd.coalCapacityLevel);
        if (TrySpend(cost))
        {
            pd.coalCapacityLevel++;
            RefreshUI();
        }
        else ShakeCost(coalCostText);
    }

    public void BuyCannon()
    {
        var pd = PlayerPartInventory.Instance.GetPD();
        int cost = GetCost(baseCannonCost, pd.cannonCapacityLevel);
        if (TrySpend(cost))
        {
            pd.cannonCapacityLevel++;
            RefreshUI();
        }
        else ShakeCost(cannonCostText);
    }

    public void BuySpeed()
    {
        var pd = PlayerPartInventory.Instance.GetPD();
        int cost = GetCost(baseSpeedCost, pd.speedUpgradeLevel);
        if (TrySpend(cost))
        {
            pd.speedUpgradeLevel++;
            RefreshUI();
        }
        else ShakeCost(speedCostText);
    }

    // ----------------------
    // 공통 유틸
    // ----------------------
    bool TrySpend(int amount)
    {
        var pd = PlayerPartInventory.Instance.GetPD();
        if (pd.gold < amount) return false;
        pd.gold -= amount;
        return true;
    }

    int GetCost(int baseCost, int level)
    {
        // 비용 = baseCost * growth^level  (반올림)
        return Mathf.RoundToInt(baseCost * Mathf.Pow(growth, level));
    }

    void RefreshUI()
    {
        var inv = PlayerPartInventory.Instance;
        var pd = inv.GetPD();
        var td = inv.GetTD(); // 상점 단독 씬이면 null일 수 있음

        if (goldText) goldText.text = $"Gold: {pd.gold:n0} G";

        if (td != null)
        {
            if (hpLevelText) hpLevelText.text = $"HP Lv.{pd.hpUpgradeLevel} (+{td.hpPerUpgrade * pd.hpUpgradeLevel})";
            if (coalLevelText) coalLevelText.text = $"Coal Lv.{pd.coalCapacityLevel} (+{td.coalPerUpgrade * pd.coalCapacityLevel})";
            if (cannonLevelText) cannonLevelText.text = $"Ammo Lv.{pd.cannonCapacityLevel} (+{td.cannonPerUpgrade * pd.cannonCapacityLevel})";
            if (speedLevelText) speedLevelText.text = $"Speed Lv.{pd.speedUpgradeLevel} (+{(td.speedPerUpgrade * pd.speedUpgradeLevel):F1})";
        }
        else
        {
            // TrainData가 없을 때는 레벨만 표기
            if (hpLevelText) hpLevelText.text = $"HP Lv.{pd.hpUpgradeLevel}";
            if (coalLevelText) coalLevelText.text = $"Coal Lv.{pd.coalCapacityLevel}";
            if (cannonLevelText) cannonLevelText.text = $"Ammo Lv.{pd.cannonCapacityLevel}";
            if (speedLevelText) speedLevelText.text = $"Speed Lv.{pd.speedUpgradeLevel}";
        }

        if (hpCostText) hpCostText.text = GetCost(baseHpCost, pd.hpUpgradeLevel).ToString("n0") + " G";
        if (coalCostText) coalCostText.text = GetCost(baseCoalCost, pd.coalCapacityLevel).ToString("n0") + " G";
        if (cannonCostText) cannonCostText.text = GetCost(baseCannonCost, pd.cannonCapacityLevel).ToString("n0") + " G";
        if (speedCostText) speedCostText.text = GetCost(baseSpeedCost, pd.speedUpgradeLevel).ToString("n0") + " G";

        UpdateButtonStates();
    }

    void UpdateButtonStates()
    {
        var pd = PlayerPartInventory.Instance.GetPD();

        int hpCost = GetCost(baseHpCost, pd.hpUpgradeLevel);
        int coalCost = GetCost(baseCoalCost, pd.coalCapacityLevel);
        int cannonCost = GetCost(baseCannonCost, pd.cannonCapacityLevel);
        int speedCost = GetCost(baseSpeedCost, pd.speedUpgradeLevel);

        if (buyHpButton) buyHpButton.interactable = (pd.gold >= hpCost);
        if (buyCoalButton) buyCoalButton.interactable = (pd.gold >= coalCost);
        if (buyCannonButton) buyCannonButton.interactable = (pd.gold >= cannonCost);
        if (buySpeedButton) buySpeedButton.interactable = (pd.gold >= speedCost);
    }

    // 부족할 때 살짝 피드백 (선택)
    void ShakeCost(Text t)
    {
        if (!t) return;
        StopAllCoroutines();
        StartCoroutine(BlinkRed(t));
    }

    IEnumerator BlinkRed(Text t)
    {
        Color orig = t.color;
        t.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        t.color = orig;
    }

    // 외부에서 골드가 변했을 때 수동 호출용
    public void ForceRefresh() => RefreshUI();
}