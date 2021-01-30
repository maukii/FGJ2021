using UnityEngine;

[ExecuteInEditMode]
public class VertexOffset : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float scale = 0.0f;
    [SerializeField] private Transform target;

    private MeshFilter filter = null;
    private MeshRenderer rend = null;
    private Vector3 center = Vector3.zero;


    private void OnValidate()
    {
        if (target == null)
            return;

        if (rend == null)
        {
            rend = target.GetComponent<MeshRenderer>();
            filter = target.GetComponent<MeshFilter>();
        }

        if(rend == null)
        {
            rend = target.GetComponentInChildren<MeshRenderer>();
            filter = target.GetComponentInChildren<MeshFilter>();
        }

        if (rend == null)
            return;

        Vector3[] vertices = filter.sharedMesh.vertices;
        center = GetCenter(vertices);
    }

    private Vector3 GetCenter(Vector3[] points)
    {
        Vector3 center = transform.position;
        foreach (Vector3 point in points)
        {
            center += point;
        }

        center /= points.Length;
        return center;
    }

    private void OnDrawGizmos()
    {
        if (center == Vector3.zero)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(center, 0.1f);
    }
}
