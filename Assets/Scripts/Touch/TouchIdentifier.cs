using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class TouchIdentifier : MonoBehaviour
{
    public int fingerId;
    public float timeCreated;
    public Vector2 startPosition;
    public Vector3 deltaPosition;
    float _time;
    public TouchDetector t;

    private void Start()
    {
        t = FindObjectOfType<TouchDetector>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //collider = collision;
        if (collision.tag == "Ground")
        {
            BaseStatus _tempStatus = collision.GetComponent<BaseStatus>();
            PlantStatus _tempPlantStatus = collision.GetComponent<PlantStatus>();

            //ebug.Log(_tempStatus);
            _tempPlantStatus.CheckIsLocked(_tempPlantStatus._spacePrice);
            GroundMangement.singleton.Isplanted(_tempStatus);
        }

        if (collision.tag == "ANimalContainer")
        {
            //Debug.Log(collision.name);
            BaseStatus _tempStatus = collision.GetComponent<BaseStatus>();
            AnimalStatus _tempAnimalStatus = collision.GetComponent<AnimalStatus>();
            //Debug.Log(_tempStatus);

            if (_tempStatus == null)
                _tempStatus = collision.GetComponentInParent<BaseStatus>();
            _tempAnimalStatus.CheckIsLocked(_tempAnimalStatus._spacePrice);
            PetManagement.singleton.IsPeted(_tempStatus);
            //Debug.Log(_tempStatus);
        }

        if (collision.tag == "Animal")
        {
            BaseStatus status = collision.GetComponentInParent<BaseStatus>();

            Debug.Log(status);
            if (status.collectCheck == true)
            {
                PetManagement.singleton._tempAnimalStatus.Collected();
                status.collectCheck = false;
            }
        }

        if (collision.tag == "Plant")
        {
            BaseStatus status = collision.GetComponentInParent<BaseStatus>();

            if (status.collectCheck)
            {
                GroundMangement.singleton._tempPlantStatus.Collected();
                status.collectCheck = false;
            }
        }

        if (collision.tag == "enemy")
        {
            EnemiesFollowing enemy = collision.GetComponent<EnemiesFollowing>();
            enemy.hp--;

            if (enemy.hp < 0)
            {
                enemy.hp = 0;
            }

            Debug.Log(enemy.hp);
        }

        if (collision.tag == "Status")
        {
            Debug.Log("hi");
            BaseStatus _tempStatus = collision.GetComponent<BaseStatus>();
            AnimalStatus _tempAnimalStatus = collision.GetComponent<AnimalStatus>();

            if (_tempStatus == null)
                _tempStatus = collision.GetComponentInParent<BaseStatus>();

            _tempStatus.isWatering();

        }

        if (collision.tag == "PlantStatus")
        {
            Debug.Log("hi");
            BaseStatus _tempStatus = collision.GetComponent<BaseStatus>();
            PlantStatus _tempPlantStatus = collision.GetComponent<PlantStatus>();

            if (_tempStatus == null)
                _tempStatus = collision.GetComponentInParent<BaseStatus>();

            _tempStatus.isWatering();
        }
    }
}
