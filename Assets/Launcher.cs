using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Called to start the game.
    public void PlayGame()
    {
        if (audioManager.isOn)
        {
            audioManager.PlaySFX(audioManager.confirm);
        }

        SceneManager.LoadSceneAsync("Game");
    }
}
