using UnityEngine;

public class DrawPointMesh : MonoBehaviour
{
    [SerializeField] private bool hideOriginalModel = true;
    private DrawConfiguration configuration;


    private void Awake()
    {
        configuration = GetComponent<DrawConfiguration>();
    }

    private void Start()
    {
        Vector3[] vertices = GetPointsFromMesh();
        Color[] colors = GetColorsFromMesh();
        configuration.CreateGameObject("PointCloud", vertices, colors, transform);
        
        if(hideOriginalModel)
        {
            MeshRenderer rend = GetComponent<MeshRenderer>();
            if (rend == null) rend = GetComponentInChildren<MeshRenderer>();
            rend.enabled = false;
        }
    }

    private Vector3[] GetPointsFromMesh()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        if (filter == null)
            filter = GetComponentInChildren<MeshFilter>();

        return filter.sharedMesh.vertices;
    }

    private Color[] GetColorsFromMesh()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        if (filter == null)
            filter = GetComponentInChildren<MeshFilter>();

        return filter.sharedMesh.colors;
    }

    #region Random
    private Vector3[] CreateRandomPoints(int count)
    {
        Vector3[] points = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            points[i] = UnityEngine.Random.insideUnitSphere;
        }

        return points;
    }
    private Color[] CreateRandomColors(int count)
    {
        Color[] colors = new Color[count];
        for (int i = 0; i < count; i++)
        {
            colors[i] = UnityEngine.Random.ColorHSV();
        }

        return colors;
    }
    #endregion
}
