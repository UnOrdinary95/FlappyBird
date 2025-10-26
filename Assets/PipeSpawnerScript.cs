using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipe;
    private static List<GameObject> pipes = new();
    public LogicScript logic;

    public float spawnRate = 3; // 3 seconds
    public float redPipeSpawnRate = 0.6f; // 60%
    private float timer = 0;
    public float heightOffset = 10;
    private float elapsedTime = 0;
    private bool secondPhase = false;
    public static float speedBoostDuration = 0f;
    private bool shieldAlreadyActivated = false;

    // spawnRate constants
    const float SP_NORMAL = 2.5f;
    const float SP_FAST = 2f;

    // moveSpeed constants
    const float MV_NORMAL = 20;
    const float MV_FAST = 30;

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPipe(secondPhase);
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.GameIsEnded()) return;

        // First Phase - SLOW
        if (!secondPhase)
        {
            if (elapsedTime >= 10f)
            {
                // Second Phase - NORMAL
                secondPhase = true;
                spawnRate = SP_NORMAL;
                foreach (GameObject pipe in pipes)
                {
                    pipe.GetComponent<PipeMoveScript>().moveSpeed = MV_NORMAL;
                }
            }
            else
            {
                elapsedTime += Time.deltaTime;
            }
        }

        // Third Phase - FAST (Temporary)
        bool thirdPhase = SpeedUp();
        if (thirdPhase)
        {
            if (audioManager.isOn)
            {
                audioManager.PlayMusic(audioManager.thirdphase);
            }

            speedBoostDuration -= Time.deltaTime;

            if (spawnRate != SP_FAST)
            {
                spawnRate = SP_FAST;
            }

            foreach (GameObject pipe in pipes)
            {
                PipeMoveScript pipeScript = pipe.GetComponent<PipeMoveScript>();
                if (pipeScript.moveSpeed != MV_FAST)
                {
                    pipeScript.moveSpeed = MV_FAST;
                }
            }
        }
        else if (secondPhase)
        {
            if (audioManager.isOn)
            {
                audioManager.PlayMusic(audioManager.secondphase);
            }

            // Back to Second Phase - NORMAL
            if (spawnRate != SP_NORMAL)
            {
                spawnRate = SP_NORMAL;
            }

            foreach (GameObject pipe in pipes)
            {
                PipeMoveScript pipeScript = pipe.GetComponent<PipeMoveScript>();
                if (pipeScript.moveSpeed != MV_NORMAL)
                {
                    pipeScript.moveSpeed = MV_NORMAL;
                }
            }
        }

        // Spawn new pipe
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPipe(secondPhase, thirdPhase);
            timer = 0;
        }

        bool currentShieldState = logic.shieldIsActive();
        if (shieldAlreadyActivated != currentShieldState)
        {
            foreach (GameObject pipe in pipes)
            {
                BoxCollider2D boxColliderTopPipe = pipe.transform.Find("Top Pipe").gameObject.GetComponent<BoxCollider2D>();
                BoxCollider2D boxColliderBottomPipe = pipe.transform.Find("Bottom Pipe").gameObject.GetComponent<BoxCollider2D>();

                boxColliderTopPipe.enabled = !currentShieldState;
                boxColliderBottomPipe.enabled = !currentShieldState;
            }
            shieldAlreadyActivated = currentShieldState;
        }
    }

    // Spawns a new pipe at a random vertical position, sets color and speed based on phase.
    void SpawnPipe(bool secondPhase, bool thirdPhase = false)
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        GameObject newPipe = Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        pipes.Add(newPipe);

        if (logic.shieldIsActive())
        {
            BoxCollider2D boxColliderTopPipe = newPipe.transform.Find("Top Pipe").gameObject.GetComponent<BoxCollider2D>();
            BoxCollider2D boxColliderBottomPipe = newPipe.transform.Find("Bottom Pipe").gameObject.GetComponent<BoxCollider2D>();

            boxColliderTopPipe.enabled = false;
            boxColliderBottomPipe.enabled = false;
        }

        if (secondPhase)
        {
            ChangePipeColor(newPipe);

            PipeMoveScript newPipeScript = newPipe.GetComponent<PipeMoveScript>();
            if (thirdPhase)
            {
                newPipeScript.moveSpeed = MV_FAST;
            }
            else
            {
                newPipeScript.moveSpeed = MV_NORMAL;
            }
        }
    }

    // Changes the color of the pipe to red with a certain probability.
    void ChangePipeColor(GameObject newPipe)
    {
        if (Random.value < redPipeSpawnRate)
        {
            // Pipe (GameObject parent)
            // - Child 1 (GameObject w/ SpriteRenderer)
            // - Child 2 (GameObject w/ SpriteRenderer)
            // Result : spriteRenderers = [sprite1, sprite2]
            SpriteRenderer[] spriteRenderers = newPipe.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer sprite in spriteRenderers)
            {
                if (sprite.tag == "TopPipe" || sprite.tag == "BottomPipe")
                {
                    sprite.color = Color.red;
                }
            }
        }
    }

    // Removes a specific pipe from the list.
    public static void RemovePipe(GameObject pipe)
    {
        pipes.Remove(pipe);
    }

    // Destroys and removes all pipes from the scene and the list.
    public static void RemovePipes()
    {
        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
        }
        pipes.Clear();
    }

    // Returns true if the speed boost is currently active.
    public static bool SpeedUp()
    {
        return speedBoostDuration > 0f;
    }
}
