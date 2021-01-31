using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeFont : MonoBehaviour
{
    public TMP_Text[] allText;
    public TMP_FontAsset defaultFont;
    public TMP_FontAsset simpleFont;
    public Toggle toggle;

    public void ChangeToSimple()
    {
        for (int i = 1; i < allText.Length; i++)
        {
            allText[i].font = toggle.isOn ? simpleFont : defaultFont;
            allText[i].color = Color.black;
        }
    }
}
