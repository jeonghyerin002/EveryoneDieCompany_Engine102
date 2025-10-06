using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TopDownCharacterController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f; // 이동 속도

    private CharacterController controller;
    private Vector3 moveDirection;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
      
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical).normalized;

      
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        if (moveDirection.magnitude > 0.1f)
        {
            transform.forward = moveDirection;
        }
    }
}
