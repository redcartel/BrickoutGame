using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] public bool demo = false;
    [SerializeField] public bool doNotAdvance = false;
    [SerializeField] public bool showStartUI = false;
    [SerializeField] public bool firstLevel = true;
    [SerializeField] public bool lastLevel = false;
    [SerializeField] public Vector2 launchVector = new Vector2(1f, 1f);
    [SerializeField] public float launchSpeed = 30f;
    [SerializeField] public float[] speeds = { 45f, 60f, 80f };

    [SerializeField] public string[] mapData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
