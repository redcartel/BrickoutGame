using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton container for global values
public class Global : MonoBehaviour
{


    [SerializeField] public int score = 0;
    [SerializeField] public int lives = 99; // for debug

    private void Start()
    {
    }

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
}
