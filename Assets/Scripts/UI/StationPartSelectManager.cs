using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationPartSelectManager : MonoBehaviour
{
    [Header("파트 선택 UI")]
    public GameObject partSelectPanel;
    public Transform partCardContainer; // 3개의 파트 카드가 들어갈 부모
    public GameObject partCardPrefab; // 파트 카드 프리팹

    [Header("파트 데이터")]
    public PartData[] allParts; // 모든 파트 목록
    private List<PartData> currentOptions = new List<PartData>(); // 현재 선택 가능한 3개

    void Start()
    {
        ShowPartSelection();
    }

    // 파트 선택 화면 표시
    public void ShowPartSelection()
    {
        partSelectPanel.SetActive(true);
        GenerateRandomParts();
        DisplayParts();
    }

    // 랜덤하게 3개 파트 선택
    void GenerateRandomParts()
    {
        currentOptions.Clear();

        List<PartData> tempList = new List<PartData>(allParts);

        for (int i = 0; i < 3 && tempList.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            currentOptions.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
        }
    }

    // 파트 카드 UI 생성
    void DisplayParts()
    {
        // 기존 카드 삭제
        foreach (Transform child in partCardContainer)
        {
            Destroy(child.gameObject);
        }

        // 새 카드 생성
        for (int i = 0; i < currentOptions.Count; i++)
        {
            GameObject card = Instantiate(partCardPrefab, partCardContainer);
            SetupPartCard(card, currentOptions[i], i);
        }
    }

    // 파트 카드 설정
    void SetupPartCard(GameObject card, PartData part, int index)
    {
        // PartCardUI 컴포넌트로 카드 내용 설정
        PartCardUI cardUI = card.GetComponent<PartCardUI>();
        if (cardUI != null)
        {
            cardUI.SetupCard(part);
        }

        // 버튼 클릭 이벤트 설정
        Button button = card.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => SelectPart(part));
        }
    }

    // 파트 선택
    void SelectPart(PartData selectedPart)
    {
        Debug.Log("선택한 파트: " + selectedPart.partName);

        // 선택한 파트를 플레이어 인벤토리에 추가
        PlayerPartInventory.Instance.AddPart(selectedPart);

        partSelectPanel.SetActive(false);

        // 선택 완료 후 다음 액션 (필요시)
        OnPartSelectionComplete();
    }

    // 파트 선택 완료 후 처리
    void OnPartSelectionComplete()
    {
        // 여기에 파트 선택 후 할 일 추가
        // 예: 다음 스테이지로 이동, 메인 메뉴 표시 등
        Debug.Log("파트 선택 완료!");
    }
}