using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] HUDController hudController;
    [SerializeField] Text scoreOutput;
    [SerializeField] Text livesOutput;
    [SerializeField] Text levelOutput;

    void FixedUpdate() {
        int score = hudController.game.score;
        int lives = hudController.game.lives;
        int level = hudController.game.level;
        scoreOutput.text = score.ToString("D6");
        livesOutput.text = lives.ToString("D2");
        levelOutput.text = level.ToString("D2");
    }
}
