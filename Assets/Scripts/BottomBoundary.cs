using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBoundary : MonoBehaviour
{
    Game game;
    [SerializeField] public int soundNum = 5;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game>();
    }

    void OnTriggerEnter2D()
    {
        game.playSound(soundNum);
        game.LoseLife();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
