using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    [Tooltip("Add popup text here")]
    [TextArea(1, 6)]
    public string text = string.Empty;
    [SerializeField] private TMP_Text textUI;
    private bool visible = false;


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
    }

    // OnClicked event
    private void OnMouseDown()
    {
        visible = !visible;
        textUI.enabled = visible;
    }
}
