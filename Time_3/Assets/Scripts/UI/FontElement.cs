using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontElement : MonoBehaviour
{
    [Header("Lista de fontes. Ordem: Dislexia, Pixelada, Sem Serifa")]
    public List<TMP_FontAsset> fonts;
    [Header("Lista de tamanhos. Ordem: Dislexia, Pixelada, Sem Serifa")]
    public List<int> fontSizes;
    private TextMeshProUGUI text;
    void Start()
    {
        FontManager.instance.OnFontChange += UpdateFont;
        text = GetComponent<TextMeshProUGUI>();
        UpdateFont();
    }
    private void UpdateFont()
    {
        var currentFont = (int)FontManager.instance.currentFont;
        text.font = fonts[currentFont];
        text.fontSize = fontSizes[currentFont];
    }
}
