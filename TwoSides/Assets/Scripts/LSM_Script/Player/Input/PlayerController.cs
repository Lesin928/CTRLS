using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;

    //임시 코드
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float dashForce = 10f;
    private bool isDashing;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Move!");
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        rb.linearVelocity = new Vector2(moveInput.x * dashForce, 0f);
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack! (X 버튼)");
    }

    public void OnShift(InputAction.CallbackContext context)
    {
        Debug.Log("Shift! (Shift 버튼)");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact! (F 버튼)");
    }
}
