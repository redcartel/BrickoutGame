using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public GameController game;
    public Dictionary<string, AudioSource> canvases = new Dictionary<string, AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameController>();
        canvases = loadDirectory();
    }

    public Dictionary<string, AudioSource> loadDirectory(bool disableStartDisabled = true)
    {
        Dictionary<string, AudioSource> newDict = new Dictionary<string, AudioSource>();
        foreach (AudioSource cnvs in FindObjectsOfType<AudioSource>())
        {
            if (cnvs.tag != "canvasContainer")
            {
                newDict.Add(cnvs.name, cnvs);
            }
        }
        return newDict;
    }
}
