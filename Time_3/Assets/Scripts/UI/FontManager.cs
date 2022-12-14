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
        DontDestroyOnLoad(gameObject);
    }

    public event Action OnFontChange;

    private void Start()
    {
        idxFont = PlayerPrefs.GetInt("Font");
        currentFont = (Font)idxFont;
        OnFontChange?.Invoke();
    }

    public void ChangeFont()
    {
        idxFont++;
        idxFont %= 3;
        currentFont = (Font)idxFont;
        PlayerPrefs.SetInt("Font", idxFont);
        OnFontChange?.Invoke();
    }
}
