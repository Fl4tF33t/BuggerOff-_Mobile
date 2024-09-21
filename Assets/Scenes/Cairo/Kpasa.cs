using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kpasa : MonoBehaviour
{
    // Original position and scale of the object
    private Vector3 originalPosition;
    private Vector3 originalScale;

    // Target scale and speed for movement and scaling
    private Vector3 targetScale = new Vector3(0.57f, 0.57f, 0.57f); // Larger scale
    public float moveSpeed = 2.0f;
    public float scaleSpeed = 2.0f;

    // Boolean to control movement and scaling
    public bool moveToCenter = true;
    public bool scaleUp = true;

    // Reference to the main camera
    private Camera mainCamera;

    private void Start()
    {
        // Store the original position and scale of the object
        originalPosition = transform.position;
        originalScale = transform.localScale;

        // Get the main camera
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Calculate the screen center position in world space
        Vector3 screenCenter = new Vector3(0.02f,0f,-2.89f);

        // Handle object movement
        if (moveToCenter)
        {
            // Move towards the screen center
            transform.position = Vector3.Lerp(transform.position, screenCenter, moveSpeed * Time.deltaTime);

            // If it reaches the center, snap to the exact position
            if (Vector3.Distance(transform.position, screenCenter) < 0.01f)
            {
                transform.position = screenCenter;
            }
        }
        else
        {
            // Move back to the original position
            transform.position = Vector3.Lerp(transform.position, originalPosition, moveSpeed * Time.deltaTime);

            // If it reaches the original position, snap to the exact position
            if (Vector3.Distance(transform.position, originalPosition) < 0.01f)
            {
                transform.position = originalPosition;
            }
        }

        // Handle object scaling
        if (scaleUp)
        {
            // Scale towards the target scale
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);

            // If it reaches the target scale, snap to the exact target scale
            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
            {
                transform.localScale = targetScale;
            }
        }
        else
        {
            // Scale back to the original scale
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, scaleSpeed * Time.deltaTime);

            // If it reaches the original scale, snap to the exact original scale
            if (Vector3.Distance(transform.localScale, originalScale) < 0.01f)
            {
                transform.localScale = originalScale;
            }
        }
    }
    }
