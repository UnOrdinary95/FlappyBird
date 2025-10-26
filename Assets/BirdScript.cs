using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody; // Reference slot to store a rigidbody2D
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererWings;
    private GameObject shield;
    public LogicScript logic;
    public float flapStrength;
    public bool isAlive = true;
    public float blinkStartThreshold = 1.0f;
    public float blinkDuration = 0.5f;
    public float blinkDurationLast = 0.05f;
    private float time = 0f;
    private bool isBlue = false;
    private bool thirdPhase;
    private bool wasInThirdPhase = false;
    private bool shieldAlreadyActivated = false;

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // #Start is called once before the first execution of Update after the MonoBehaviour is created
    // Any code that will run as soon as this script is enabled (it runs once)
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRendererWings = transform.Find("BirdWings").gameObject.GetComponent<SpriteRenderer>();
        shield = gameObject.transform.Find("Shield").gameObject;
    }

    // #Update is called once per frame
    // Any code that will run as soon as this script is enabled (it runs constantly)
    void Update()
    {
        if (logic.GameIsEnded()) return; // No more update if the game is ended

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && isAlive) // If left click or spacebar has been pressed on this frame
        {
            myRigidBody.linearVelocity = Vector2.up * flapStrength; // Vector2.up = (0,1) 
        }

        if (transform.position.y > 17 || transform.position.y < -17)
        {
            logic.GameOver();
        }

        thirdPhase = PipeSpawnerScript.SpeedUp();

        if (thirdPhase)
        {
            if (!wasInThirdPhase)
            {
                wasInThirdPhase = true;
            }

            if (PipeSpawnerScript.speedBoostDuration > blinkStartThreshold)
            {
                blink(blinkDuration);
            }
            else
            {
                blink(blinkDurationLast);
            }

            bool currentShieldState = logic.shieldIsActive();
            if (shieldAlreadyActivated != currentShieldState)
            {
                audioManager.PlaySFX(audioManager.shield);
                shield.SetActive(currentShieldState);
                shieldAlreadyActivated = currentShieldState;
            }
        }
        else if (wasInThirdPhase)
        {
            resetColor();
            shield.SetActive(false);
            shieldAlreadyActivated = false;
            wasInThirdPhase = false;
        }
    }

    // Called when the bird collides with another object (e.g., ground or pipe).
    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.GameOver();
        isAlive = false;
    }

    // Makes the bird blink between blue and default colors at a given interval.
    void blink(float blinkDuration)
    {
        time += Time.deltaTime;
        if (time >= blinkDuration)
        {
            time = 0;
            isBlue = !isBlue;
            spriteRenderer.color = isBlue ? Color.blue : Color.white; // Color.white => Default color
            spriteRendererWings.color = isBlue ? Color.blue : Color.white;
        }
    }

    // Resets the bird's color to white (default).
    void resetColor()
    {
        spriteRenderer.color = Color.white;
        spriteRendererWings.color = Color.white;
    }
}
