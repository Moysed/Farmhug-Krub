using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.AI;
public class EnemiesFollowing : MonoBehaviour
{
    Vector2 Selfpos;
    public GameObject plant;
    public float speed;
    public int hp;
    [SerializeField] float timer;

    [SerializeField] int leaveCheck = 0;

    /*private Camera mainCamera;
    public Vector2 widthThresold;
    public Vector2 heightThresold;*/


    
    float distance;
    void Start()
    {
        timer = 0;
        hp = 5;
        Selfpos = new Vector2(Random.Range(10,20) , Random.Range(10,20));
        transform.position = Selfpos;
    }

    // Update is called once per frame
    void Update()
    {

        if(leaveCheck == 2)
        {
            Debug.Log("Out of Edge");
            Destroy(this.gameObject);
            leaveCheck = 0;
        }

        if (plant.gameObject.active == true)
        {
            //timer += Time.deltaTime;
            distance = Vector2.Distance(transform.position, plant.transform.position);
            Vector2 direction = plant.transform.position - transform.position;

            //Go to Plant
            transform.position = Vector2.MoveTowards(this.transform.position, plant.transform.position, speed * Time.deltaTime);

            if(hp <= 0)
            {
                Destroy(gameObject, 0.1f);
            }
        }

        else if(plant.gameObject.active == false)
        {
                //Go Back
                transform.position = Vector2.MoveTowards(this.transform.position, Selfpos, speed * Time.deltaTime);

                /*Vector2 screenPosition = mainCamera.WorldToScreenPoint (transform.position);
                if (screenPosition.x < widthThresold.x || screenPosition.x > widthThresold.y || screenPosition.y < heightThresold.x || screenPosition.y > heightThresold.y)
                Destroy (gameObject);*/
                
                
                /*Renderer thisRender = GetComponentInChildren<Renderer>();
            if (!thisRender.isVisible)
            {
                Destroy(gameObject);
                Debug.Log("Destroyed by invisibility");
            }*/
        }

        /*if(timer >= 11)
        {
            timer = 0;
        }*/
        
        /* if(timer <= 0)
        {
            Destroy(this.gameObject);
            timer = 10;
        } */
    }

    private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "CamEdge")
            {
                leaveCheck += 1;
            }
        }

    /*void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }*/
}
