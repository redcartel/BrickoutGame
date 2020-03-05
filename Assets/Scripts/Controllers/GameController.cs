using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private Global global;

    [SerializeField] public float stageWidth = 144f;
    [SerializeField] public float expectedHeight = 256f;
    [SerializeField] public float expectedWidth = 144f;
    [SerializeField] public float borderWidth = 0f;

    [SerializeField] public int levelOneSceneNo = 1;
    [SerializeField] public int gameOverSceneNo = 0;
    [SerializeField] public int maxScore = 1000000;

    [SerializeField] public int blockCount = 0; // serialized for debugging, don't change

    public bool gamePlaying = false;
    public bool autoPlay = false;
    public bool waitToChangeBallVelocity = false; // fix weird ball behavior

    public int score { get { return global.score; } private set { global.score = value; } }
    public int lives { get { return global.lives; } private set { global.lives = value; } }

    [SerializeField] public LevelData levelData;
    [SerializeField] public HUDController hudController;
    [SerializeField] public LevelController levelController;
    [SerializeField] public SoundController soundController;
    [SerializeField] public ForegroundController foregroundController;
    [SerializeField] public InputController inputController;

    public float mouseX { get { return inputController.effectiveMouseX; } }
    public float mouseY { get { return inputController.effectiveMouseY; } }

    // Attempt to find controllers that have not been specified.
    private void LoadControllers()
    {
        global = FindObjectOfType<Global>();
        levelData = (levelData is null) ? FindObjectOfType<LevelData>() : levelData;
        hudController = (hudController is null) ? FindObjectOfType<HUDController>() : hudController;
        levelController = (levelController is null) ? FindObjectOfType<LevelController>() : levelController;
        soundController = (soundController is null) ? FindObjectOfType<SoundController>() : soundController;
        foregroundController = (foregroundController is null) ? FindObjectOfType<ForegroundController>() : foregroundController;
        inputController = (inputController is null) ? FindObjectOfType<InputController>() : inputController;
    }

    private void Start()
    {
        Debug.Log("GameController start");
        LoadControllers();
        if (levelData.firstLevel)
        {
            ResetPlayerData();
        }
        levelController.PopulateLevel(levelData);
    }

    private void Update()
    {
        // UpdateScaleFactor();
    }

    public void ResetPlayerData()
    {
        score = 0;
        lives = global.startingLives;
    }

    public void ResetGame(int buildIndex=0)
    {
        global.score = 0;
        global.lives = global.startingLives;
        SceneManager.LoadScene(buildIndex);
    }

    public void WinLevel()
    {
        // In a normal level, advance to the next stage, otherwise reload this stage
        if (levelData.goToNextStage)
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

    public void IncreaseScore(int amount)
    {
        score += amount;
        // hudControl.Refresh();
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
        lives--;
        if (lives <= 0)
        {
            LoseGame();
        }
        else
        {
            foregroundController.ResetBall();
        }
    }

    public void LoseGame()
    {
    }

    public void playSound(int soundNumber)
    {
    }

    public void HandleMouseMove(float eX, float eY)
    {
        foregroundController.HandleMouseMove(eX, eY);
    }

    public void HandleClick(float eX, float eY)
    {
        if (foregroundController.stuckToPaddle)
        {
            foregroundController.LaunchBall(levelData.launchVector, levelData.launchSpeed);
        }
    }

    public void SetSpeedClass(int speedClass)
    {
        float newMinBallSpeed = levelData.speeds[speedClass];
        foregroundController.MinBallSpeed(newMinBallSpeed);
    }
}