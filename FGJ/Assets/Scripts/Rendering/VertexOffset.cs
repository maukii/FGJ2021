using UnityEngine;

public class VertexOffset : MonoBehaviour
{
    [SerializeField] private Vector3 scale = Vector3.one;
    private bool init = false;
    private MeshFilter filt;
    private Vector3[] originalVertices;



    private void OnValidate()
    {
        if (!init)
        {
            filt = GetComponent<MeshFilter>();
            if (filt == null)
                filt = GetComponentInChildren<MeshFilter>();

            originalVertices = filt.sharedMesh.vertices;
            init = true;
        }

        Vector3[] vertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            vertices[i] = originalVertices[i] + Vector3.Scale(filt.sharedMesh.normals[i], scale);
        }

        filt.sharedMesh.vertices = vertices;
    }

    private void OnDisable()
    {
        init = false;
    }
}
