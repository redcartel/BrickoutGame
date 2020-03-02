using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update
    Text scoreOutput;
    Text livesOutput;
    Text levelOutput;
    Global global;
    Game game;
    
    void Start()
    {
        scoreOutput = GameObject.Find("ScoreOutput").GetComponent<Text>();
        livesOutput = GameObject.Find("LivesOutput").GetComponent<Text>();
        levelOutput = GameObject.Find("LevelOutput").GetComponent<Text>();
        global = FindObjectOfType<Global>();
        game = FindObjectOfType<Game>();
    }

    void Update()
    {
        scoreOutput.text = global.score.ToString();
        livesOutput.text = global.lives.ToString();
        if (!game.normalLevel)
        {
            scoreOutput.text = "------";
            livesOutput.text = "--";
            levelOutput.text = "--";
        }
        else
        {
            scoreOutput.text = global.score.ToString();
            livesOutput.text = global.lives.ToString();

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            levelOutput.text = currentSceneIndex.ToString();
        }
    }
}
