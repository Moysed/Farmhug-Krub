using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    Camera _Camera;

    public Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
    public RaycastHit hit;
    public Touch myTouch;

    // Update is called once per frame
    public void Detect()
    {
        if (Input.touches.Length > 0) 
        { 
            myTouch = Input.GetTouch(0); 
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
            /*if (myTouch.phase == TouchPhase.Began) 
            { 
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
            } */
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
