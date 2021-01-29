using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 direction = new Vector3(0, -1, 0);
    [SerializeField] private float speed = 5f;


    private void Update()
    {
        transform.Rotate(direction, speed * Time.deltaTime);
    }
}
