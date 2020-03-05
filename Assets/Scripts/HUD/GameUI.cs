using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    /*
    // Start is called before the first frame update
    Text scoreOutput;
    Text livesOutput;
    Text levelOutput;
    Game game;
    
    private void Start()
    {
        scoreOutput = GameObject.Find("ScoreOutput").GetComponent<Text>();
        livesOutput = GameObject.Find("LivesOutput").GetComponent<Text>();
        levelOutput = GameObject.Find("LevelOutput").GetComponent<Text>();
        game = FindObjectOfType<Game>();
    }


    private void Update()
    {
        if (!game.normalLevel)
        {
            scoreOutput.text = "------";
            livesOutput.text = "--";
            levelOutput.text = "--";
        }
        else
        {
            scoreOutput.text = game.global.score.ToString("D6");
            livesOutput.text = game.global.lives.ToString("D2");
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            levelOutput.text = currentSceneIndex.ToString("D2");
        }
    }
    */
}
