using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    /* 
     * Singleton Pattern
     */

    private void Awake()
    {
        int globalCount = FindObjectsOfType<Global>().Length;
        if (globalCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    /*
     * End pattern
     */

    [SerializeField] public int score = 0;
    [SerializeField] public int lives = 99; // for debug
    [SerializeField] public int level = 99;
    [SerializeField] public int startingLives = 3;
    [SerializeField] public float targetHeightPx = 1024f;
}
