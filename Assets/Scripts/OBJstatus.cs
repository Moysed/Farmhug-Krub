using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJstatus : MonoBehaviour
{
    public float growTime;
    public GameObject Prefab;
    private GameObject obj;
    public Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for a tap on the screen
        growTime -= Time.deltaTime;
        if (growTime <= 0)
        {
            growTime = 10;
            if (obj == null)
            {
                obj = Instantiate(Prefab, pos, Quaternion.identity);
                Debug.Log("Create");
            }
        }

        /* else if (Input.touchCount > 0 && obj != null)
        {
            Touch t = Input.GetTouch(0);
            if(t.phase == TouchPhase.Began)
            {
                // Convert touch position to world space
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(t.position);

                // Check if the touch hits the specified game object
                Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);

                if (hitCollider != null && hitCollider.gameObject == obj)
                {
                    // Do something when a collision is detected with the specific game object
                    Debug.Log("Collision with the specific game object: " + obj.name);

                    DestroyThisObject();
                    Debug.Log("Using DestroyThisObject");
                }
            }
        } 

         else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && obj != null)
        {
            growTime = 10;
            DestroyThisObject();
            Debug.Log("Using DestroyThisObject");
        } */
    }

    public void DestroyThisObject()
    {
        // Destroy the game object this script is attached to
        Destroy(obj);
        obj = null;
    }
}
