using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    [SerializeField] public Vector2 launchVector = new Vector2(1f, 1f);
    [SerializeField] public float launchSpeed = 2.0f;
    [SerializeField] public float[] speeds = { 2.5f, 3.0f, 3.5f };

    [SerializeField] public int blockCount = 0; // serialized for debugging, don't change
    [SerializeField] public bool autoPlay = false;
    [SerializeField] public bool normalLevel = true; // display
    [SerializeField] public float expectedPixelHeight = 1024f;

    public bool gamePlaying = false; // when false, ball is stuck to paddle ready to launch
    public bool frozen = false; // stop moving paddle

    public Ball ball;
    public Paddle paddle;
    public Global global;
    private Sounds sounds;
    private int updateTick = 0;

    public bool waitToChangeBallVelocity = false; // fix weird ball behavior
    private List<CanvasScaler> UIcScalerList = new List<CanvasScaler>();
    private float oldScaleFactor = 1.0f;


    // Start is called before the first frame update
    private void Start()
    {
        launchVector = launchVector.normalized * launchSpeed;
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        global = FindObjectOfType<Global>();
        sounds = FindObjectOfType<Sounds>();

        // Create a List of each UI Canvas' CanvasScaler
        Canvas[] canvasArray = FindObjectsOfType<Canvas>();
        for (int i = 0; i < canvasArray.Length; i++)
        {
            CanvasScaler cScaler = canvasArray[i].GetComponent<CanvasScaler>();
            if (cScaler != null)
            {
                UIcScalerList.Add(cScaler);
            }
        }
    }

    private void Update()
    {
        UpdateScaleFactor();
    }

    // Update is called once per frame
    private void UpdateScaleFactor()
    {
        float newScaleFactor = Screen.height / expectedPixelHeight;
        if (newScaleFactor != oldScaleFactor)
        {
            oldScaleFactor = newScaleFactor;
            foreach (CanvasScaler cScaler in UIcScalerList)
            {
                cScaler.scaleFactor = newScaleFactor;
            }
        }
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
        // In a normal level, advance to the next stage, otherwise reload this stage
        if (normalLevel)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            int thisSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(thisSceneIndex);
        }
    }

    public void IncreaseScore(int byAmount)
    {
        if (normalLevel)
        {
            global.score += byAmount;

            // Can flip the score like in Mario
            if (global.score >= 1000000)
            {
                global.score = global.score % 1000000;
            }
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

    public void playSound(int soundNumber)
    {
        if (gamePlaying && normalLevel)
        {
            sounds.sounds[soundNumber].Play();
        }
    }
}
