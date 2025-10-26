using UnityEngine;

public class OpenGitHubLink : MonoBehaviour
{
    public string url = "";
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Opens the specified URL in the browser.
    public void OpenLink()
    {
        if (audioManager.isOn)
        {
            audioManager.PlaySFX(audioManager.confirm);
        }

        Application.OpenURL(url);
    }
}
