using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerBossInstance Boss => PlayerDataManager.Instance.Boss;

    [SerializeField]
    float rotSpeed = 0.15f;
    [SerializeField]
    float walkSpeed = 6f;
    [SerializeField]
    float sprintSpeed = 7.5f;

    float currSpeed;

    Vector2 dir;
    Vector3 currVel;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currSpeed = walkSpeed;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    public void OnMovementInput(InputAction.CallbackContext ctx)
    {
        dir = ctx.ReadValue<Vector2>();
    }

    public void OnShiftHeld(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
            currSpeed = walkSpeed;
        else 
            currSpeed = sprintSpeed;
    }


    void MovePlayer()
    {
        // Make player walk
        Vector3 movement = new Vector3(Mathf.Round(dir.x), 0, Mathf.Round(dir.y));
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, movement.normalized * currSpeed, ref currVel, 0.1f);

        // Rotate player (and prevent snapping back to default)
        if (movement.sqrMagnitude > 0.01f)
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), rotSpeed);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
}
