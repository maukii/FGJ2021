using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class EditorPropertyBlock : MonoBehaviour
{
    public Renderer rend;
    public float randSizeScalar = 0.1f;
    public float pointRadius = 0.075f;
    public bool renderCircles = true;
    public bool screenSize = false;

    private MaterialPropertyBlock block;


    private void Awake()
    {
        if (rend == null)
            rend = GetComponent<Renderer>();
        if (rend == null)
            rend = GetComponentInChildren<Renderer>();

        if (rend != null)
        {
            rend.material = screenSize ? new Material(Shader.Find("Custom/QuadGeoScreenSizeShader")) : new Material(Shader.Find("Custom/QuadGeoWorldSizeShader"));
        }

        if (block == null)
            block = new MaterialPropertyBlock();

        rend?.GetPropertyBlock(block);
        block.SetFloat("_RandSize", !screenSize ? randSizeScalar : randSizeScalar * 100);
        block.SetFloat("_PointSize", !screenSize ? pointRadius : pointRadius * 100);
        block.SetInt("_Circles", renderCircles ? 1 : 0);

        if (screenSize)
        {
            block.SetFloat("_ScreenWidth", Screen.width);
            block.SetFloat("_ScreenHeight", Screen.height);
        }

        rend?.SetPropertyBlock(block);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
            return;

        Awake();
    }
#endif
}
