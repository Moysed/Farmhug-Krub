using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public GameObject PrefabObject;
    Vector2 PrefabPos;
    GameObject obj;
    Camera _Camera;
 
    void Start()
    {
        _Camera = this.GetComponent<Camera>();
        PrefabPos = Vector2.zero;
        obj = Instantiate(PrefabObject, PrefabPos, Quaternion.identity);
    }
 
    void Update()
    {
        // Check if there is any touch input
        if (Input.touchCount > 0)
        {
            // Loop through all the touches
            for (int i = 0; i < Input.touchCount; i++)
            {
                // Get the current touch
                Touch touch = Input.GetTouch(i);
 
                // Check if the touch phase is began (user touched the screen)
                if (touch.phase == TouchPhase.Began)
                {
                    // Convert the touch position to a ray
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
 
                    // Create a RaycastHit variable to store information about the hit
                    RaycastHit hit;
 
                    // Check if the ray hits any collider
                    if (Physics.Raycast(ray, out hit))
                    {
                        // Check if the collider belongs to a game object with a specific tag
                        if (hit.collider.CompareTag("Animal"))
                        {
                            // Do something when the touch hits an object with the specified tag
                            Debug.Log("Touched an object with the tag 'Animal'");
 
                            // Destroy the object
                            Destroy(obj);
 
                            // Instantiate a new object at a different position
                            PrefabPos = Vector2.zero;
                            obj = Instantiate(PrefabObject, PrefabPos, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }
}
