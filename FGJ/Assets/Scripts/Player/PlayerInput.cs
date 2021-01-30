using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool raw = true;

    public float Horizontal => horizontal;
    private float horizontal = 0f;

    public float Vertical => vertical;
    private float vertical = 0f;

    public Vector2 InputDirection => new Vector2(horizontal, vertical).normalized;

    public float MouseX => mouseX;
    private float mouseX = 0f;

    public float MouseY => mouseY;
    private float mouseY = 0f;

    public Vector2 MouseInput => new Vector2(mouseX, mouseY);


    private void Update()
    {
        if (raw)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");       
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }
}
