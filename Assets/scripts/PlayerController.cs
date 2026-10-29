using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    [Header("Movement")]
    public float speed = 10f;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;
    public GameObject loseTextObject;

    [Header("Game Rules")]
    public int totalPickups = 6; // Set this to the total number of PickUps in your scene

    private int score = 0;
    private bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        SetScoreText();

        if (winTextObject != null) winTextObject.SetActive(false);
        if (loseTextObject != null) loseTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        if (isGameOver) return;
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        if (isGameOver) return;
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        // Collectible pickup
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            score++;
            SetScoreText();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isGameOver) return;

        // Hitting Enemy or Danger Obstacle results in Game Over
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle"))
        {
            TriggerGameOver(false);
        }
    }

    void SetScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }

        if (score >= totalPickups)
        {
            TriggerGameOver(true);
        }
    }

    void TriggerGameOver(bool won)
    {
        isGameOver = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (won)
        {
            if (winTextObject != null) winTextObject.SetActive(true);
        }
        else
        {
            if (loseTextObject != null) loseTextObject.SetActive(true);
        }
    }
}