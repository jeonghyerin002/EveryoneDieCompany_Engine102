using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainEngine : InteractableObject
{
    [Header("속도 증가 설정")]
    public float speedBoostPerCoal = 1f;
    public float speedBoostDuration = 5f;

    public override void Interact()
    {
        if (InteractionManager.Instance.TryUseItem(InteractionManager.ItemType.Coal))
        {
            GameManager.Instance.StartSpeedBoost(speedBoostPerCoal, speedBoostDuration);
            Debug.Log("석탄 투입! 속도 증가!");
        }
        else
        {
            Debug.Log("석탄을 들고 있지 않습니다!");
        }
    }
}
