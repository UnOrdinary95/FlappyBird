using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public Text highScoreText;
    public Text nbCoinsText;
    public GameObject gameOverScreen;
    private bool gameEnded = false;
    public int nb_coins = 0;
    private static float shieldDuration = 0f;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (audioManager.isOn)
        {
            audioManager.PlayMusic(audioManager.secondphase);
        }
    }

    void Start()
    {
        highScoreText.text = $"High Score : {PlayerPrefs.GetInt("HighScore", 0)}";
    }

    void Update()
    {
        if (GameIsEnded()) return;

        NbCoinsToText();
        checkIfEnoughCoins();

        if (shieldIsActive())
        {
            shieldDuration -= Time.deltaTime;
        }
    }

    [ContextMenu("Increase Score")] // Debugging tool for Unity
    // Adds the specified score to the player's total and updates the score display.
    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    // Updates the coin count display.
    public void NbCoinsToText()
    {
        nbCoinsText.text = $"{nb_coins} / 10";
    }

    // Restarts the game, resets relevant states, and reloads the current scene.
    public void RestartGame()
    {
        if (audioManager.isOn)
        {
            audioManager.PlaySFX(audioManager.confirm);
        }

        Debug.ClearDeveloperConsole();
        PipeSpawnerScript.speedBoostDuration = 0f;
        PipeSpawnerScript.RemovePipes();
        resetCoinScript();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Handles game over logic, updates high score, and shows the game over screen.
    public void GameOver()
    {
        if (audioManager.isOn)
        {
            audioManager.PlaySFX(audioManager.gameover);
        }

        if (playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            PlayerPrefs.Save();
        }
        gameOverScreen.SetActive(true);
        gameEnded = true;
    }

    // Returns true if the game has ended.
    public bool GameIsEnded()
    {
        return gameEnded;
    }

    // Checks if the player has collected enough coins for a bonus and applies effects.
    void checkIfEnoughCoins()
    {
        if (nb_coins >= 10)
        {
            nb_coins -= 10;
            PipeSpawnerScript.speedBoostDuration += 6f;
            shieldDuration += 6f;
        }
    }

    // Resets the coin count and shield duration.
    public void resetCoinScript()
    {
        nb_coins = 0;
        shieldDuration = 0f;
    }

    // Returns true if the shield is currently active.
    public bool shieldIsActive()
    {
        return shieldDuration > 0f;
    }
}
