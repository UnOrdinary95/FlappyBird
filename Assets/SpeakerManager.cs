using UnityEngine;
using UnityEngine.UI;

public class SpeakerManager : MonoBehaviour
{
    public RawImage rawImage;
    public Texture2D img_on;
    public Texture2D img_off;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Toggles the speaker state, updates music and icon accordingly.
    public void ToggleSpeaker()
    {
        audioManager.isOn = !audioManager.isOn;
        ToggleMainMenuTheme();
        ToggleImage();
    }

    // Plays or stops the main menu music based on the speaker state.
    public void ToggleMainMenuTheme()
    {
        if (audioManager.isOn)
        {
            audioManager.PlayMusic(audioManager.mainmenu);
        }
        else
        {
            audioManager.StopMusic();
        }
    }

    // Updates the speaker icon image based on the audio state.
    public void ToggleImage()
    {
        rawImage.texture = audioManager.isOn ? img_on : img_off;
    }
}
