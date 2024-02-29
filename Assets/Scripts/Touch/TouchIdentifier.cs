using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class TouchIdentifier : MonoBehaviour {
    public int fingerId;
    public float timeCreated;
    public Vector2 startPosition;
    public Vector3 deltaPosition;

    /*private AnimalStatus animal;

    public void Start()
    {
        animal = GetComponent<AnimalStatus>();
    }*/
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //collider = collision;
        if (collision.tag == "Ground")
        {
            //animal.CheckIsLocked(animal._spacePrice);

            //Debug.Log(collision.name);
            BaseStatus _tempStatus = collision.GetComponent<BaseStatus>();

            //ebug.Log(_tempStatus);
            GroundMangement.singleton.Isplanted(_tempStatus);
        }

        if(collision.tag == "ANimalContainer")
        {
            Debug.Log(collision.name);
            BaseStatus _tempStatus = collision.GetComponent<BaseStatus>();
            //Debug.Log(_tempStatus);

            if(_tempStatus == null)
                _tempStatus = collision.GetComponentInParent<BaseStatus>();

            PetManagement.singleton.IsPeted(_tempStatus);
            //Debug.Log(_tempStatus);
        }

        if(collision.tag == "Animal")
        {
           BaseStatus status = collision.GetComponentInParent<BaseStatus>();

            Debug.Log(status);
            if (status.collectCheck == true)
            {
                

                PetManagement.singleton._tempAnimalStatus.Collected();
                status.collectCheck = false;
            }
                
        }
    }
}
