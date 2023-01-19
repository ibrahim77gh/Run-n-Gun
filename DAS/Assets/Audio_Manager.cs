using System;
using UnityEngine;
using UnityEngine.Audio;

public class Audio_Manager : MonoBehaviour
{
    public Sound[] sounds;
    private static Audio_Manager instance;

    // Start is called before the first frame update
    void Awake()
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

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, item => item.soundName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        else
        {
            s.source.Play();
        }
    }
}