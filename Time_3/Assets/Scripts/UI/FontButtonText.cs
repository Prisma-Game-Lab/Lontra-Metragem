using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontButtonText : MonoBehaviour
{
    [Header("Ordem: Dislexia, Pixelada, Sem Serifa")]
    public List<string> fontNames;
    private TextMeshProUGUI text;
    void Start()
    {
        FontManager.instance.OnFontChange += UpdateText;
        text = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }
    private void UpdateText()
    {
        var currentFont = (int)FontManager.instance.currentFont;
        text.text = fontNames[currentFont];
    }
}
