using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileFarmManager : MonoBehaviour
{
    /* public GameObject cropPrefab;
    public Transform plantingArea;
    public int maxCrops = 10;

    private int currentCrops = 0;
    private bool isTouching = false;

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch is on the planting area
            if (touch.phase == TouchPhase.Began && IsTouchOnPlantingArea(touch.position))
            {
                isTouching = true;
                PlantCrop(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isTouching = false;
            }
        }

        Debug.Log(currentCrops);
    }

    void PlantCrop(Vector2 touchPosition)
    {
        // Convert touch position to world space
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 10f));
        worldPosition.z = 0f;

        // Instantiate a new crop at the touch position
        GameObject newCrop = Instantiate(cropPrefab, worldPosition, Quaternion.identity);

        // Set the parent to the planting area for organization
        newCrop.transform.parent = plantingArea;

        currentCrops++;
    }

    bool IsTouchOnPlantingArea(Vector2 touchPosition)
    {
        // Raycast to check if the touch is on the planting area
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchPosition), Vector2.zero);
        return hit.collider != null && hit.collider.transform == plantingArea;
    }

    // Call this method when a crop is ready to be harvested
    public void HarvestCrop()
    {
        currentCrops--;
        // Add logic for handling resources, updating UI, etc.
    } */

    public GameObject cropPrefab;
    public Transform plantingArea;
    public int maxCrops = 10;

    private int currentCrops = 0;
    private bool isTouching = false;

    void Update()
    {
        HandleTouchInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isTouching = IsTouchOnPlantingArea(touch.position);
                    if (isTouching && currentCrops < maxCrops)
                    {
                        PlantCrop(touch.position);
                    }
                    break;

                case TouchPhase.Ended:
                    isTouching = false;
                    break;
            }
        }
    }

    void PlantCrop(Vector2 touchPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 10f));
        worldPosition.z = 0f;

        GameObject newCrop = Instantiate(cropPrefab, worldPosition, Quaternion.identity);
        newCrop.transform.parent = plantingArea;

        currentCrops++;
    }

    bool IsTouchOnPlantingArea(Vector2 touchPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchPosition), Vector2.zero);
        return hit.collider != null && hit.collider.transform == plantingArea;
    }
}
