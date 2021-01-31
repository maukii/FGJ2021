using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    [Tooltip("Add popup text here")]
    [TextArea(1, 6)]
    public string text = string.Empty;
    [SerializeField] private TMP_Text textUI;
    private bool visible = false;

    [Header("Highlighting")]
    [SerializeField] private float defaultSize = 0.015f;
    [SerializeField] private float highlightedSize = 0.03f;
    private MeshRenderer rend;
    private MaterialPropertyBlock block;


    private void OnValidate()
    {
        if(textUI != null)
        {
            textUI.SetText(text);
        }
    }

    private void Awake()
    {
        textUI = GetComponentInChildren<TMP_Text>();
        textUI.SetText(text);
        textUI.enabled = visible;

        if (block == null)
            block = new MaterialPropertyBlock();
    }

    private void Highlight(bool enabled)
    {
        rend = transform.Find("PointCloud").GetComponent<MeshRenderer>();
        rend.GetPropertyBlock(block);
        block.SetFloat("_RandSize", !enabled ? defaultSize : highlightedSize);
        block.SetFloat("_PointSize", !enabled ? defaultSize : highlightedSize);
        rend.SetPropertyBlock(block);
    }

    private void OnMouseEnter()
    {
        Highlight(true);
    }

    private void OnMouseExit()
    {
        Highlight(false);
    }

    // OnClicked event
    private void OnMouseDown()
    {
        visible = !visible;
        textUI.enabled = visible;
    }
}
