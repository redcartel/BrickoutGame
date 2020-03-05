using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] public bool demo = false;
    [SerializeField] public bool goToNextStage = true;
    [SerializeField] public bool firstLevel = true;
    [SerializeField] public bool lastLevel = false;
    [SerializeField] public Vector2 launchVector = new Vector2(1f, 1f);
    [SerializeField] public float launchSpeed = 2.0f;
    [SerializeField] public float[] speeds = { 2.5f, 3.0f, 3.5f };

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
