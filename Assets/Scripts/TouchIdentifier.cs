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
            BaseStatus _tempStatus = collision.GetComponent<BaseStatus>();

            Debug.Log(_tempStatus);
            GroundMangement.singleton.Isplanted(_tempStatus);
        }

        if(collision.tag == "Animal")
        {
            Debug.Log(collision.name);
            BaseStatus _tempStatus = collision.GetComponent<BaseStatus>();

            Debug.Log(_tempStatus);
            PetManagement.singleton.IsPeted(_tempStatus);
            Debug.Log(_tempStatus);
        }

    }
}
