using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;

    [Header("플레이어")]
    public Transform player;

    [Header("들고 있는 아이템")]
    private ItemType holdingItem = ItemType.None;

    [Header("상호작용 설정")]
    public float interactionRange = 2f;
    public LayerMask interactionLayer;

    [Header("현재 상호작용 가능한 오브젝트")]
    private InteractableObject currentInteractable = null;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }


    void Update()
    {
        CheckNearbyInteractable();

        if(Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }
    void CheckNearbyInteractable()
    {
        Collider[] colliders = Physics.OverlapSphere(player.position, interactionRange, interactionLayer);

        float closestDistance = interactionRange;
        InteractableObject closestInteractable = null;

        foreach (Collider col in colliders)
        {
            InteractableObject interactable = col.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(player.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }
        currentInteractable = closestInteractable;
    }
    void TryInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
    public void PickupItem(ItemType item)
    {
        if (holdingItem == ItemType.None)
        {
            holdingItem = item;
            Debug.Log(item + "획득!");
        }
    }
    public bool TryUseItem(ItemType requiredItem)
    {
        if (holdingItem == requiredItem)
        {
            holdingItem = ItemType.None;
            Debug.Log(requiredItem + "사용!");
            return true;
        }
        return false;
    }
    public ItemType GetHoldingItem()
    {
        return holdingItem;
    }
    public bool IsHoldingItem()
    {
        return holdingItem != ItemType.None;
    }
    public bool CanInteract()
    {
        return currentInteractable != null;
    }
    public enum ItemType
    { 
        None,
        Cannonball,
        Coal
    }

}
