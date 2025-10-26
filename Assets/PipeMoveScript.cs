using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 3;
    public float deadZone = -45;
    public LogicScript logic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.GameIsEnded()) return;

        // Move pipe left at constant speed regardless of frame rate
        // Time.deltaTime ensures movement is frame-rate independent
        transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            PipeSpawnerScript.RemovePipe(gameObject);
            Destroy(gameObject); // Destroy the gameobject that holds this script
        }
    }
}
