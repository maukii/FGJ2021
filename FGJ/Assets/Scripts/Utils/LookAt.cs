using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private bool inverted = false;
    private Camera camera;


    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (inverted)
        {
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        }
        else
        {
            transform.LookAt(target);
        }
    }
}
