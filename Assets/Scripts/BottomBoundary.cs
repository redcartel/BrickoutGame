using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBoundary : MonoBehaviour
{
    Game game;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game>();
    }

    void OnTriggerEnter2D()
    {
        game.LoseLife();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
