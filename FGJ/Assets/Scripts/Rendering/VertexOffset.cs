using UnityEngine;

public class VertexOffset : MonoBehaviour
{
    [SerializeField] private Vector3 scale = Vector3.one;
    private bool init = false;
    private MeshFilter filt;
    private Vector3[] originalVertices;    
    private Mesh mesh;


    private void OnValidate()
    {
        if (!init)
        {
            filt = GetComponent<MeshFilter>();
            if (filt == null)
                filt = GetComponentInChildren<MeshFilter>();

            originalVertices = filt.sharedMesh.vertices;
            mesh = new Mesh();
            mesh.vertices = filt.sharedMesh.vertices;
            mesh.normals = filt.sharedMesh.normals;
            mesh.colors = filt.sharedMesh.colors;
            int[] indices = new int[mesh.vertices.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = i;
            }

            mesh.SetIndices(indices, MeshTopology.Points, 0);
            init = true;
        }

        Vector3[] vertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            vertices[i] = originalVertices[i] + Vector3.Scale(filt.sharedMesh.normals[i], scale);
        }

        mesh.vertices = vertices;
        filt.sharedMesh = mesh;
    }

    private void OnDisable()
    {
        init = false;
    }
}
