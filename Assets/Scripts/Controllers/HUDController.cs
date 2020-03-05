using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameController game;
    public Dictionary<string, Canvas> canvases = new Dictionary<string, Canvas>();
    private const string containerTag = "canvasContainer";
    private const string disabledTag = "defaultDisabled";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HUDController Start");
        game = FindObjectOfType<GameController>();
        canvases = loadDirectory();
        disableDefaults();
    }

    // populate a dictionary of name : GameObject pairs of Canvas elements without the canvasContainer
    public Dictionary<string, Canvas> loadDirectory(bool disableStartDisabled = true)
    {
        Dictionary<string, Canvas> newDict = new Dictionary<string, Canvas>();
        foreach (Canvas cnvs in FindObjectsOfType<Canvas>())
        {
            if (cnvs.tag != containerTag)
            {
                newDict.Add(cnvs.name, cnvs);
            }
        }
        return newDict;
    }

     public void disableDefaults()
    {
        foreach (string key in canvases.Keys)
        {
            Debug.Log("disableDefaults " + key);
            if (canvases[key].tag == disabledTag)
            {
                canvases[key].enabled = false;
            }
        }
    }
}
