using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public enum Font
{
    Dyslexia,
    Pixel,
    SansSerif

}
public class FontManager : MonoBehaviour
{
    [HideInInspector]
    public static FontManager instance;
    public Font currentFont;
    private int idxFont;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public event Action OnFontChange;

    public void ChangeFont()
    {
        idxFont++;
        idxFont %= 3;
        currentFont = (Font)idxFont;
        OnFontChange?.Invoke();
    }
}
