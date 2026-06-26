using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public float YClamp = 85f;
    public float Sensitivity = 10f;

    private PlayerController player;
    private Vector2 camInputs;
    private float xRotation = 0f;
    private Vector2 currentLook;
    private Vector2 lookVelocity;

    public float smoothTime = 0.05f;


    private bool autoLookActive = false;
    private Transform autoLookTarget;
    private Vector3 offsetAutoLook;
    public float autoLookSpeed = 5f;

    public bool invertY = false;

    bool cameraLocked;


    void Start()
    {
        player = GetComponentInParent<PlayerController>();

        InputManager.Instance.Inputs.Player.Look.performed += ctx => camInputs = ctx.ReadValue<Vector2>();
        InputManager.Instance.Inputs.Player.Look.canceled += ctx => camInputs = Vector2.zero;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (!cameraLocked)
        {
            if (autoLookActive && autoLookTarget != null)
            {
                HandleAutoLook();
            }
            else
            {
                HandlePlayerLook();
            }
        }
    }

    public void LockCamera(bool locked)
    {
        cameraLocked = locked;
    }

    void HandlePlayerLook()
    {
        currentLook = Vector2.SmoothDamp(currentLook, camInputs, ref lookVelocity, smoothTime);

        float mouseX = currentLook.x * Sensitivity * Time.deltaTime;
        float mouseY = currentLook.y * Sensitivity * Time.deltaTime;

        xRotation -= invertY ? -mouseY : mouseY;
        xRotation = Mathf.Clamp(xRotation, -YClamp, YClamp);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        player.transform.Rotate(Vector3.up * mouseX);
    }


    void HandleAutoLook()
    {
        Vector3 direction = (autoLookTarget.position + offsetAutoLook) - transform.position;
        direction.Normalize();

        // Get target angles
        float targetX = Mathf.Asin(direction.y) * Mathf.Rad2Deg;
        float targetY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // Smooth rotation
        xRotation = Mathf.Lerp(xRotation, -targetX, Time.deltaTime * autoLookSpeed);

        float currentY = player.transform.eulerAngles.y;
        float newY = Mathf.LerpAngle(currentY, targetY, Time.deltaTime * autoLookSpeed);

        player.transform.rotation = Quaternion.Euler(0f, newY, 0f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void StartAutoLook(Transform target, Vector3 offset = new())
    {
        autoLookTarget = target;
        offsetAutoLook = offset;
        autoLookActive = true;
    }

    public void StopAutoLook()
    {
        autoLookActive = false;
    }

}
