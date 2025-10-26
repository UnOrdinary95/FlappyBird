using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public LogicScript logic;
    private AudioManager audioManager;

    // called once before Start()
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

    }

    // Called when another collider enters the coin's trigger zone.
    // Handles scoring, coin counting, and coin destruction.
    void OnTriggerEnter2D(Collider2D collision)
    {
        audioManager.PlaySFX(audioManager.coin);
        if (gameObject.tag == "Coin")
        {
            logic.AddScore(15);
            logic.nb_coins++;
            Destroy(gameObject);
        }
        else
        {
            logic.AddScore(45);
            logic.nb_coins += 3;
            Destroy(gameObject);
        }
    }
}
