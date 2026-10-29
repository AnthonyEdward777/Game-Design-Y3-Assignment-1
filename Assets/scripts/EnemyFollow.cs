using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float chaseSpeed = 3.5f;
    public float detectionRange = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= detectionRange)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            direction.y = 0; // Keep movement along the ground plane

            // Move toward player
            rb.AddForce(direction * chaseSpeed);
        }
    }
}