using UnityEngine;
using TMPro;

public class CoalInteraction : MonoBehaviour
{
    public int maxCoal = 10;
    public int currentCoal = 0;
    public float coalConsumptionRate = 1f;
    public GameObject infoUI;
    public TextMeshProUGUI coalStorageText;
    public TextMeshProUGUI playerCoalText;

    private bool playerInRange = false;
    private PlayerInventory playerInventory;
    private float consumptionTimer = 0f;

    void Start()
    {
        if (infoUI != null)
        {
            infoUI.SetActive(false);
        }
    }

    void Update()
    {
        ConsumeCoal();

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SupplyCoal();
        }

        if (playerInRange)
        {
            UpdateUI();
        }
    }

    void ConsumeCoal()
    {
        if (currentCoal <= 0) return;

        consumptionTimer += Time.deltaTime;

        if (consumptionTimer >= coalConsumptionRate)
        {
            consumptionTimer = 0f;
            currentCoal--;
        }
    }

    void UpdateUI()
    {
        if (coalStorageText != null)
        {
            coalStorageText.text = "ºÆ≈∫ ¿˙¿Âº“: " + currentCoal + " / " + maxCoal;
        }

        if (playerCoalText != null && playerInventory != null)
        {
            playerCoalText.text = "∫∏¿Ø ºÆ≈∫: " + playerInventory.coal;
        }
    }

    void SupplyCoal()
    {
        if (playerInventory == null) return;

        if (currentCoal >= maxCoal)
        {
            Debug.Log("ºÆ≈∫ ¿˙¿Âº“∞° ∞°µÊ √°Ω¿¥œ¥Ÿ");
            return;
        }

        if (playerInventory.coal <= 0)
        {
            Debug.Log("ºÆ≈∫¿Ã æ¯Ω¿¥œ¥Ÿ");
            return;
        }

        playerInventory.coal--;
        currentCoal++;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerInventory = other.GetComponent<PlayerInventory>();

            if (infoUI != null)
            {
                infoUI.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (infoUI != null)
            {
                infoUI.SetActive(false);
            }
        }
    }
}