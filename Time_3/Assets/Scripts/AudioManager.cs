using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.audio;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string nome)
    {
        Sounds s = Array.Find(sounds, sound => sound.nome == nome);
        if(s == null)
        {
            return;
        }
        s.source.Play();
    }

}
