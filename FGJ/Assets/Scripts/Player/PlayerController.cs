using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerControllerSettings settings;
    [SerializeField] private Camera cam = null;

    private CharacterController controller = null;
    private PlayerInput input = null;
    private Transform cameraTransform;
    private float xRotation = 0f;

    private AudioSource audioSource;
    private float nextFootstep = 1f;
    private float footStepDelay = 0.6f;
    private float runStepDelay = 0.3f;
    public AudioClip[] footSteps;

    bool playerIsLocked = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        audioSource = GetComponent<AudioSource>();
        cameraTransform = cam.transform;

        if (settings.lockCursor)
        {
            LockPlayer();
        }
    }

    public void LockPlayer()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerIsLocked = true;
        controller.enabled = false;
    }
    
    public void ReleasePlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerIsLocked = false;
        controller.enabled = true;
    }

    private void Update()
    {
        if (!playerIsLocked)
        {
            bool sprint = Input.GetKey(settings.sprintKey);

            Move(sprint);
            Look();
        }

    }

    private void Move(bool sprint)
    {
        Vector2 inputDirection = input.InputDirection;
        Vector3 direction = transform.forward * inputDirection.y + transform.right * inputDirection.x;
        controller.SimpleMove(direction * (!sprint ? settings.walkSpeed : settings.runSpeed) * Time.deltaTime);
        
        if (nextFootstep <= 0 && controller.velocity.magnitude > 0.5f)
        {
            audioSource.PlayOneShot(footSteps[Random.Range(0, footSteps.Length)], 0.7f);
            nextFootstep += (!sprint ? footStepDelay : runStepDelay);   
        }
        if (controller.velocity.magnitude > 0.1f)
        {
            nextFootstep -= Time.deltaTime;        
        }
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