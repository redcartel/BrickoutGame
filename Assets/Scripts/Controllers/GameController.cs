using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private Global global;

    [SerializeField] public float expectedHeight = 256f;
    [SerializeField] public float expectedWidth = 144f;
    [SerializeField] public float borderWidth = 0f;

    [SerializeField] public int levelOneSceneNo = 1;
    [SerializeField] public int maxScore = 1000000;
    [SerializeField] public int startingLives = 3;

    [SerializeField] public string loseLifeSoundTag = "DIE";
    [SerializeField] public string gameOverSoundTag = "GAMEOVER";
    [SerializeField] public string gameStartSoundTag = "START";
    [SerializeField] public string gameWonSoundTag = "";

    [SerializeField] public int blockCount = 0; // serialized for debugging, don't change

    public bool gamePlaying = false;
    public bool autoPlay = false;
    public bool waitToChangeBallVelocity = false; // fix weird ball behavior

    public int score { get { return global.score; } private set { global.score = value; } }
    public int lives { get { return global.lives; } private set { global.lives = value; } }
    public int level 
    { 
        get { return SceneManager.GetActiveScene().buildIndex - levelOneSceneNo + 1;} 
    }

    [SerializeField] public bool demoMode = false;

    [SerializeField] public LevelData levelData;
    [SerializeField] public HUDController hudController;
    [SerializeField] public LevelController levelController;
    [SerializeField] public SoundController soundController;
    [SerializeField] public ForegroundController foregroundController;
    [SerializeField] public InputController inputController;

    public float mouseX { get { 
        if (!demoMode) {
            return inputController.effectiveMouseX;
        } else {
            return foregroundController.ball.transform.position.x;
        }
    } }
    public float mouseY { get { return inputController.effectiveMouseY; } }

    private float rememberedMouseX = 0.0f;
    private float rememberedMouseY = 0.0f;

    // Attempt to find controllers that have not been specified.

    private void Start()
    {
        global = FindObjectOfType<Global>();
        // hudController.DisableDefaults();
        levelController.PopulateLevel(levelData);
        hudController.RefreshGameUI();

        if (levelData.firstLevel)
        {
            ResetPlayerData();
        }
        if (levelData.demo)
        {
            demoMode = true;
        }
        if (levelData.showStartUI)
        {
            hudController.ShowStartMessage();
        }
    }

    private void Update()
    {
        if (!(inputController is null))
        {
            inputController.UpdateCheckInput();
        }
        if ((new Vector2(mouseX, mouseY) - new Vector2(rememberedMouseX, rememberedMouseY)).magnitude > 1) {
            HandleMouseMove(mouseX, mouseY);
        }
    }

    public void HandleMouseMove(float eX, float eY)
    {
    }

    public void HandleClick(float eX, float eY)
    {
        foregroundController.HandleClick(eX, eY, levelData.launchVector, levelData.launchSpeed);
        hudController.HandleClick(eX, eY);
    }

    public void ResetPlayerData()
    {
        score = 0;
        lives = startingLives;
    }

    public void ResetGame(int sceneNo=0)
    {
        ResetPlayerData();
        SceneManager.LoadScene(sceneNo);
    }

    public void StartGame()
    {
        ResetPlayerData();
        SceneManager.LoadScene(levelOneSceneNo);
    }

    public void WinLevel()
    {
        Debug.Log("WinLevel");
        // In a normal level, advance to the next stage, otherwise reload this stage
        if (levelData.lastLevel)
        {
            foregroundController.Freeze();
            hudController.ShowWinMessage();
        }
        else if (!levelData.doNotAdvance)
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
        hudController.RefreshGameUI();
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
    public void LoseLife(string soundTag="")
    {
        lives--;
        hudController.RefreshGameUI();
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            foregroundController.ResetPaddle();
            foregroundController.ResetBall();
            foregroundController.stuckToPaddle = true;
            PlaySound(loseLifeSoundTag);
        }
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        PlaySound(gameOverSoundTag);
        foregroundController.Freeze();
        hudController.ShowGameOver();
    }
    public void PlaySound(string soundTag)
    {
        soundController.PlaySound(soundTag);
    }
    public void SetBallSpeedClass(int speedClass)
    {
        foregroundController.ball.SetMinMaxSpeeds(levelData.speeds[speedClass]);
    }
}