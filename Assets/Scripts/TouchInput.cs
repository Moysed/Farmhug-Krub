using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{

    Camera _Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length > 0) 
        { 
            Touch myTouch = Input.GetTouch(0); 
            if (myTouch.phase == TouchPhase.Began) 
            { 
                /*if (obj == null) 
                { 
                    obj = Instantiate (PrefabSprite); 
                    obj.name = myTouch.fingerId.ToString(); 
                    Vector3 newPosition = _Camera.ScreenToWorldPoint (myTouch.position); 
                    newPosition.z = 0; obj.transform.position = newPosition; 
                    SpriteRenderer spr = obj.GetComponent<SpriteRenderer>(); 
                    if (spr != null) 
                    { 
                        spr.color = new Color (Random.Range (0, 100) / 100f, Random.Range (0, 100) / 100f, Random.Range (0, 100) / 100f);
                    } 
                } */
            } 
            
            if (myTouch.phase == TouchPhase.Moved) 
            { 
                /*if (obj != null) 
                { 
                    if (obj.name.Contains(myTouch.fingerId.ToString()))
                    { 
                        Vector3 newPosition = _Camera.ScreenToWorldPoint (myTouch.position); 
                        newPosition.z = 0; 
                        obj.transform.position = newPosition; 
                    } 
                }*/
            } 
            if (myTouch.phase == TouchPhase.Ended || myTouch.phase == TouchPhase.Canceled) 
            { 
                /*if (obj != null)
                { 
                    if (obj.name.Contains(myTouch.fingerId.ToString())) 
                    { 
                        Destroy (obj.gameObject, 0.5f); 
                        obj = null; 
                    } 
                }*/
            } 
        }
    }
}
