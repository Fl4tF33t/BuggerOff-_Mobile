using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WheelManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 dragStartPos;
    private bool isDragging = false;

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
            transform.Rotate(Vector3.forward, -delta.x/3); // Rotate the wheel based on drag delta
            dragStartPos = eventData.position; // Update drag start position
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Calculate the closest snap point and snap if necessary
            SnapToNearestSnapPoint();
            isDragging = false;
        }
    }

   
    void SnapToNearestSnapPoint()
    {
        float snapInterval = 45f; // The interval between snap points

        float rotationAngle = transform.eulerAngles.z;
        float snappedRotation = Mathf.Round(rotationAngle / snapInterval) * snapInterval;

        transform.rotation = Quaternion.Euler(0f, 0f, snappedRotation);
    }

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
