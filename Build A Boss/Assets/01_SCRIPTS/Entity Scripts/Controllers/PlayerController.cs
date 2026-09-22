using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //private BossProfileInstance Boss => GameManager.Instance.PlayerInstance;

    [Header("Movement Controls")]
    float currSpeed;
    [SerializeField]
    float walkSpeed = 6f;
    [SerializeField]
    float sprintSpeed = 7.5f;

    Vector2 dir;
    Vector3 currVel;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currSpeed = walkSpeed;
        PlayerRefGetter.Instance.PlayerInstance.GameObjectInstance = gameObject;

        InputManager.Instance.OnPlayerMovementInput += OnMove;
        InputManager.Instance.OnPlayerSprintInput += OnSprint;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnPlayerMovementInput -= OnMove;
        InputManager.Instance.OnPlayerSprintInput -= OnSprint;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    public void OnMove(Vector2 _val)
    {
        dir = _val;
    }

    public void OnSprint(bool _performed)
    {
        if (!_performed)
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
