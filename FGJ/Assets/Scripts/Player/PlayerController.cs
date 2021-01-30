using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerControllerSettings settings;

    private CharacterController controller = null;
    private PlayerInput input = null;
    private Transform cameraTransform;
    private Camera cam = null;
    private float xRotation = 0f;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        cam = GetComponentInChildren<Camera>();
        if (cam == null)
            CreateCamera();

        cam.transform.position = new Vector3(0, controller.height, 0);
        cameraTransform = cam.transform;

        if (settings.lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void CreateCamera()
    {
        GameObject cameraObject = new GameObject("CameraObject", typeof(Camera));
        cameraObject.transform.SetParent(transform);
        cam = cameraObject.GetComponent<Camera>();
    }

    private void Update()
    {
        bool sprint = Input.GetKey(settings.sprintKey);

        Move(sprint);
        Look();
    }

    private void Move(bool sprint)
    {
        Vector2 inputDirection = input.InputDirection;
        Vector3 direction = transform.forward * inputDirection.y + transform.right * inputDirection.x;
        controller.SimpleMove(direction * (!sprint ? settings.walkSpeed : settings.runSpeed) * Time.deltaTime);
    }

    private void Look()
    {
        float mouseX = input.MouseX * settings.mouseSensitivity * Time.deltaTime;
        float mouseY = input.MouseY * settings.mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}

[System.Serializable]
public struct PlayerControllerSettings
{
    public bool lockCursor;
    public float mouseSensitivity;
    public float walkSpeed;
    public float runSpeed;
    public KeyCode sprintKey;
}