using UnityEngine;

public class DrawProcedural : MonoBehaviour
{
    private struct VertexData
    {
        public Vector3 position;
        public Color color;
    }

    [Range(1, 5000000)]
    [SerializeField] private int instanceCount = 60000;
    [SerializeField] private Material material;
    [SerializeField] private Transform root;

    private int cachedInstanceCount = -1;
    private ComputeBuffer vertexBuffer;
    private bool initialized = false;
    private Vector3[] points;


    private void OnValidate()
    {
        if (vertexBuffer == null) 
            return;

        if (cachedInstanceCount != instanceCount)
            UpdateBuffers();
    }

    private void OnEnable()
    {
        if (initialized)
            OnDisable();

        GetPoints();
        UpdateBuffers();
        initialized = true;
    }

    private void GetPoints()
    {
        Transform[] childs = root.GetComponentsInChildren<Transform>();
        points = new Vector3[childs.Length];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new Vector3(childs[i].position.x, childs[i].position.y, childs[i].position.z);
        }
    }

    private void Update()
    {
        if (cachedInstanceCount != instanceCount)
            UpdateBuffers();

        // Render
        Graphics.DrawProcedural(material, new Bounds(Vector3.zero, new Vector3(100f, 100f, 100f)), MeshTopology.Points, points.Length);
    }

    void UpdateBuffers()
    {        
        vertexBuffer?.Release();
        vertexBuffer = new ComputeBuffer(points.Length, sizeof(float) * 3 + sizeof(float) * 4);
        VertexData[] data = new VertexData[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            data[i] = new VertexData()
            {
                position = points[i],
                color = Random.ColorHSV(),
            };
        }

        vertexBuffer.SetData(data);
        material.SetBuffer("_VertexBuffer", vertexBuffer);
        cachedInstanceCount = instanceCount;
    }

    void OnDisable()
    {
        vertexBuffer?.Release();
        vertexBuffer = null;
        initialized = false;
    }
}
