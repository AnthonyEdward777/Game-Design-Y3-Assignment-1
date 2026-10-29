using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Vector3 moveDirection = new Vector3(3, 0, 0);
    public float moveSpeed = 2f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Moves back and forth using a sine wave
        float offset = Mathf.Sin(Time.time * moveSpeed);
        transform.position = startPosition + (moveDirection * offset);
    }
}