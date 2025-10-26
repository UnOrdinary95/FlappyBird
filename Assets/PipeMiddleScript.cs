using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    public LogicScript logic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called when another collider enters the middle pipe's trigger zone.
    // Handles scoring and speed boost logic if the top pipe is red.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3) // If the gameObject's layer is Bird
        {
            logic.AddScore(10);

            // Get current GameObject (that this Script is attached to)
            GameObject middleGameObject = gameObject;

            // Get parent Transform (Parent)
            Transform parentPipeTransform = middleGameObject.transform.parent;

            // Get Top Pipe Transform (Child)
            Transform topPipeTransform = parentPipeTransform.transform.Find("Top Pipe");

            // Get Top Pipe SpriteRenderer
            SpriteRenderer topPipeSpriteRenderer = topPipeTransform.gameObject.GetComponent<SpriteRenderer>();

            if (topPipeSpriteRenderer != null && topPipeSpriteRenderer.color == Color.red)
            {
                PipeSpawnerScript.speedBoostDuration += 4f;
            }
        }
    }
}
