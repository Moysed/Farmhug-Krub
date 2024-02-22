using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class TouchIdentifier : MonoBehaviour {
    public int fingerId;
    public float timeCreated;
    public Vector2 startPosition;
    public Vector3 deltaPosition;
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //collider = collision;
        if (collision.tag == "Ground")
        {
            Debug.Log(collision.name);
            PlantStatus _tempPlantStatus = collision.GetComponent<PlantStatus>();

            Debug.Log(_tempPlantStatus);
            GroundMangement.singleton.Isplanted(_tempPlantStatus);
        }
    }
}
