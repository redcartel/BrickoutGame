using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] public Vector2 launchVector = new Vector2(4f, 4f);


    [SerializeField] public int blockCount = 0; // serialized for debugging, don't change
    [SerializeField] public bool autoPlay = false;
    [SerializeField] public bool normalLevel = true;

    public bool gamePlaying = false; // when false, ball is stuck to paddle ready to launch
    public bool frozen = false; // stop moving paddle

    public Ball ball;
    public Paddle paddle;
    public Vector3 paddleBallDelta;
    public Global global;
    public Canvas gameOverUI;


    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        global = FindObjectOfType<Global>();
        paddleBallDelta = ball.transform.position - paddle.transform.position; // TODO: refactor this into Ball
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        global.score = 0;
        global.lives = global.startingLives;
        SceneManager.LoadScene(1);

    }

    public void ResetGame()
    {
        global.score = 0;
        global.lives = global.startingLives;
        SceneManager.LoadScene(0);
    }

    public void WinLevel()
    {
        if (normalLevel)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void IncreaseScore(int byAmount)
    {
        global.score += byAmount;

        // Can flip the score like in Mario
        while (global.score >= 1000000)
        {
            global.score -= 1000000;
        }
    }

    public void AddBlock()
    {
        blockCount++;
    }

    public void RemoveBlock()
    {
        blockCount--;
        if (blockCount <= 0)
        {
            WinLevel();
        }
    }

    public void LoseLife()
    {
        global.lives--;
        if (global.lives > 0)
        {
            gamePlaying = false;
        }
    }
}
