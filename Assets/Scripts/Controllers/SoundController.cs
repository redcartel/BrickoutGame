using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public GameController game;
    public Dictionary<string, AudioSource> directory = new Dictionary<string, AudioSource>();

    [SerializeField] public string[] soundTags;
    [SerializeField] public AudioSource[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SoundController Start");
        int stop = sounds.Length > soundTags.Length ? sounds.Length : soundTags.Length;
        for (int i = 0; i < stop; i++)
        {
            directory.Add(soundTags[i], sounds[i]);
        }
    }

    public void PlaySound(string soundTag)
    {
        if (directory.ContainsKey(soundTag))
        {
            directory[soundTag].Play();
        }
        else
        {
            Debug.LogWarning(string.Format("soundTag {0} not found. (SoundController)", soundTag));
        }
    }
}