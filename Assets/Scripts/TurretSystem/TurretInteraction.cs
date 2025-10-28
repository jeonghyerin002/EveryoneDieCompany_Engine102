using UnityEngine;
using TMPro;

public class TurretInteraction : MonoBehaviour
{
    public int maxBullets = 5;
    public int currentBullets = 0;

    public GameObject infoUI;
    public TextMeshProUGUI turretBulletText;
    public TextMeshProUGUI playerBulletText;

    private bool playerInRange = false;
    private PlayerInventory playerInventory;

    void Start()
    {
        if (infoUI != null)
        {
            infoUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SupplyBullet();
        }

        if (playerInRange)
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (turretBulletText != null)
        {
            turretBulletText.text = "ÅÍ·¿ Æ÷Åº: " + currentBullets + " / " + maxBullets;
        }

        if (playerBulletText != null && playerInventory != null)
        {
            playerBulletText.text = "º¸À¯ Æ÷Åº: " + playerInventory.bullets;
        }
    }

    void SupplyBullet()
    {
        if (playerInventory == null) return;

        if (currentBullets >= maxBullets)
        {
            Debug.Log("ÅÍ·¿ Æ÷ÅºÀÌ °¡µæ Ã¡½À´Ï´Ù");
            return;
        }

        if (playerInventory.bullets <= 0)
        {
            Debug.Log("Æ÷ÅºÀÌ ¾ø½À´Ï´Ù");
            return;
        }

        playerInventory.bullets--;
        currentBullets++;
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