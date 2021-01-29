using System;
using System.Collections.Generic;
using UnityEngine;

public class PointMesh : MonoBehaviour
{
    [SerializeField] private DrawConfiguration configuration;
    [SerializeField] private bool useAchor = true;
    [SerializeField] private Transform root;

    private Vector3[] data;
    private Vector3 anchor;



    private void Start()
    {
        data = GetPointsFromMesh();

        if (useAchor)
            data = AnchorToFirst(data);

        Color[] colors = GetColorsFromMesh();
        configuration.CreateGameObject("PointCloud", data, colors, transform);
    }

    private Vector3[] GetPointsFromMesh() => root.GetComponent<MeshFilter>().sharedMesh.vertices;
    private Color[] GetColorsFromMesh() => root.GetComponent<MeshFilter>().sharedMesh.colors;

    private Vector3[] GetPointsFromChilds()
    {
        Transform[] childs = root.GetComponentsInChildren<Transform>();
        Vector3[] results = new Vector3[childs.Length];
        for (int i = 0; i < childs.Length; i++)
        {
            results[i] = childs[i].position;
        }

        return results;
    }

    private Vector3[] AnchorToFirst(Vector3[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (useAchor)
            {
                if (i == 0)
                {
                    anchor = data[i];
                    data[i] = Vector3.zero;
                    continue;
                }

                data[i] = anchor - data[i];
            }
        }

        return data;
    }

    private List<Vector3[]> Split(Vector3[] points)
    {
        List<Vector3[]> result = new List<Vector3[]>();

        Vector3[] set = new Vector3[Mathf.Min(configuration.GetMaximumPointsPerMesh(), points.Length)];
        int index = 0;
        for (int i = 0; i < points.Length; i++)
        {
            if (index == configuration.GetMaximumPointsPerMesh())
            {
                result.Add(set);
                set = new Vector3[Mathf.Min(configuration.GetMaximumPointsPerMesh(), points.Length - i)];
                index = 0;
            }

            set[index] = points[i];
            index++;
        }

        return result;
    }

    private void CreateRandom()
    {
        Vector3[] v = CreateRandomPoints(100);
        Color[] c = CreateRandomColors(100);
        configuration.CreateGameObject("Random", v, c, transform);
    }

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

    private void CreateMesh()
    {
        GameObject obj = new GameObject("PointMesh");
        obj.transform.SetParent(transform);
        var mRenderer = obj.AddComponent<MeshRenderer>();
        mRenderer.material = new Material(Shader.Find("Custom/PointShader"));
        mRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mRenderer.receiveShadows = false;

        var mFilter = obj.AddComponent<MeshFilter>();
        var mesh = mFilter.mesh = new Mesh();

        var colors = new Color[]
        {
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
            UnityEngine.Random.ColorHSV(),
        };
        var vertices = new Vector3[]
        {
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
            UnityEngine.Random.insideUnitSphere,
        };
        int[] indecies = new int[vertices.Length];
        for (int i = 0; i < vertices.Length; ++i)
        {
            indecies[i] = i;
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.SetIndices(indecies, MeshTopology.Points, 0);
        mesh.RecalculateBounds();
    }
}
