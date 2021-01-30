using UnityEngine;

[RequireComponent(typeof(DrawConfiguration))]
public class DrawPointMesh : MonoBehaviour
{
    [SerializeField] private float colorRandomizing = 0.2f;
    [SerializeField] private bool over65000Vertices = false;
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
        RandomizeColors(colors);

        configuration.CreateGameObject("PointCloud", vertices, colors, transform, !over65000Vertices ? UnityEngine.Rendering.IndexFormat.UInt16 : UnityEngine.Rendering.IndexFormat.UInt32);
        
        if(hideOriginalModel)
        {
            MeshRenderer rend = GetComponent<MeshRenderer>();
            if (rend == null) rend = GetComponentInChildren<MeshRenderer>();
            rend.enabled = false;
        }
    }

    private void RandomizeColors(Color[] colors)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = new Color()
            {
                r = colors[i].r + Random.Range(-colorRandomizing, colorRandomizing),
                g = colors[i].g + Random.Range(-colorRandomizing, colorRandomizing),
                b = colors[i].b + Random.Range(-colorRandomizing, colorRandomizing),
                a = colors[i].a,
            };
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
