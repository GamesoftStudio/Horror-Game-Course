using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 3f;

    public AudioSource footstepSource;
    public AudioClip[] footsteps;

    public float stepInterval = 0.5f;

    private float stepTimer;

    private CharacterController controller;
    Vector2 moveInput;
    private float moveSpeed;
    bool run;
    bool canMove = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        InputManager.Instance.Inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        InputManager.Instance.Inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        InputManager.Instance.Inputs.Player.Sprint.performed += ctx => run = true;
        InputManager.Instance.Inputs.Player.Sprint.canceled += ctx => run = false;
        moveSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
            moveSpeed = run ? runSpeed : walkSpeed;
            if(!controller.isGrounded) move.y = -2f;
            controller.Move(move.normalized * moveSpeed * Time.deltaTime);
        }

        HandleFootsteps();
        
    }

    public bool IsMoving()
    {
        return controller.velocity.magnitude > 0;
    }

    public void LockControlls(bool ct)
    {
        canMove = ct;
    }

    void HandleFootsteps()
    {
        if (!IsMoving())
        {
            stepTimer = 0f;
            return;
        }

        stepTimer += Time.deltaTime;

        float interval = stepInterval / moveSpeed;

        if (stepTimer >= interval)
        {
            PlayFootstep();
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footsteps.Length == 0) return;

        int index = Random.Range(0, footsteps.Length);

        footstepSource.PlayOneShot(footsteps[index]);
    }

}
