using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBoundary : MonoBehaviour
{
    GameController game;
    [SerializeField] public int soundNum = 5;

    void Start()
    {
        game = FindObjectOfType<GameController>();
    }

    void OnTriggerEnter2D()
    {
        game.LoseLife();

        Debug.Log("Life lost");
    }

    void Update()
    {
        
    }
}
