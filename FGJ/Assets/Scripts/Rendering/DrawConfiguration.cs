using UnityEngine;
using UnityEngine.Rendering;

public class DrawConfiguration : MonoBehaviour
{
    public float randSizeScalar = 0.1f;
    public float pointRadius = 0.075f;
    public bool renderCircles = true;
    public bool screenSize = false;

    private Camera renderCamera = null;
    private Material material = null;


    private void LoadShaders()
    {
        if (screenSize)
            material = new Material(Shader.Find("Custom/QuadGeoScreenSizeShader"));
        else
            material = new Material(Shader.Find("Custom/QuadGeoWorldSizeShader"));

        material.SetFloat("_RandSize", randSizeScalar);
        material.SetFloat("_PointSize", pointRadius);
        material.SetInt("_Circles", renderCircles ? 1 : 0);
    }

    public void Awake()
    {
        renderCamera = Camera.main;
        LoadShaders();
    }

    public void Update()
    {
        if (screenSize)
        {
            Rect screen = renderCamera.pixelRect;
            material.SetInt("_ScreenWidth", (int)screen.width);
            material.SetInt("_ScreenHeight", (int)screen.height);
        }
    }

    public GameObject CreateGameObject(string name, Vector3[] vertexData, Color[] colorData, Transform parent, IndexFormat indexFormat)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        MeshFilter filter = go.AddComponent<MeshFilter>();
        Mesh mesh = filter.mesh = new Mesh();
        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.shadowCastingMode = ShadowCastingMode.Off;
        renderer.receiveShadows = false;
        renderer.material = material;
        mesh.indexFormat = indexFormat;

        int[] indecies = new int[vertexData.Length];
        for (int i = 0; i < vertexData.Length; i++)
        {
            indecies[i] = i;
        }

        mesh.vertices = vertexData;
        mesh.colors = colorData;
        mesh.SetIndices(indecies, MeshTopology.Points, 0);

        return go;
    }

    public int GetMaximumPointsPerMesh()
    {
        return 65000;
    }
}
