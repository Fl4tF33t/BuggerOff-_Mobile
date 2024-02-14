using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WheelManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform[] snapPoints; // Array of snap points
    public Transform singularPoint; // Reference point for snapping on the rim
    public float snapThreshold = 5f; // Threshold angle for snapping

    private Vector2 dragStartPos;
    private bool isDragging = false;
    private float totalRotation = 0f; // Accumulated rotation angle during dragging

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Check if the drag starts within the bounds of the wheel
        if (IsPointerOverUIObject(eventData.position))
        {
            dragStartPos = eventData.position;
            isDragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 delta = eventData.position - dragStartPos;
            totalRotation -= delta.x/3; // Accumulate rotation (subtracting to correct direction)
            transform.Rotate(Vector3.forward, -delta.x/3); // Rotate the wheel based on drag delta
            dragStartPos = eventData.position; // Update drag start position
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("enddrag");
        if (isDragging)
        {
            Debug.Log("Does it check?");


            // Calculate the closest snap point and snap if necessary
            SnapToNearestSnapPoint();
            isDragging = false;
        }
    }

   
    void SnapToNearestSnapPoint()
    {
        Debug.Log(this.gameObject);
        float rotationAngle = transform.eulerAngles.z;

        if (rotationAngle >= 0 && rotationAngle < 22.5f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        else if (rotationAngle >= 22.5f && rotationAngle < 67.5f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
        }
        else if (rotationAngle >= 67.5f && rotationAngle < 112.5f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
        }
        else if (rotationAngle >= 112.5f && rotationAngle < 157.5f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 135f));
        }
        else if (rotationAngle >= 157.5f && rotationAngle < 202.5f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        }
        else if (rotationAngle >= 202.5f && rotationAngle < 247.5f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 225f));
        }
        else if (rotationAngle >= 247.5f && rotationAngle < 292.5f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
        }
        else if (rotationAngle >= 292.5f && rotationAngle < 337.5f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 315f));
        }
        else if (rotationAngle >= 337.5f && rotationAngle < 360f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    }

    //void SnapTo(float targetAngle)
    //{
    //    float deltaAngle = targetAngle - totalRotation; // Calculate the difference in angle
    //    totalRotation += deltaAngle; // Update total rotation
    //    transform.Rotate(Vector3.forward, deltaAngle); // Rotate the wheel to the snap point
    //}

    // Helper method to check if the pointer is over the wheel UI
    bool IsPointerOverUIObject(Vector2 touchPosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = touchPosition;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }
}
