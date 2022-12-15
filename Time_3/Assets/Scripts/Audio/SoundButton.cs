using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    public List<GameObject> points;
    private static string masterSoundPrefs = "Master";
    private static string soundEffectsPrefs = "SFX";
    private static string musicPrefs = "Music";
    private int soundValueIdx;

    void Start()
    {
        SetSoundPrefs();
        SetPoints();
    }

    public void UpdateVolume()
    {
        soundValueIdx++;
        soundValueIdx %= 4;

        if (gameObject.tag == "Master")
            PlayerPrefs.SetInt(masterSoundPrefs, soundValueIdx);
        else if (gameObject.tag == "Music")
            PlayerPrefs.SetInt(musicPrefs, soundValueIdx);
        else
            PlayerPrefs.SetInt(soundEffectsPrefs, soundValueIdx);

        SetPoints();
        AudioManager.instance.UpdateSoundVolumes();
    }

    private void SetSoundPrefs()
    {
            if (gameObject.tag == "Master")
                soundValueIdx = PlayerPrefs.GetInt(masterSoundPrefs);
            else if (gameObject.tag == "Music")
                soundValueIdx = PlayerPrefs.GetInt(musicPrefs);
            else
                soundValueIdx = PlayerPrefs.GetInt(soundEffectsPrefs);         
    }
    
    private void SetPoints()
    {
        foreach (GameObject p in points)
        {
            if (points.IndexOf(p) < soundValueIdx)
                p.SetActive(true);
            else
                p.SetActive(false);
        }
    }
}
