using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemiesFollowing : MonoBehaviour
{
    Vector2 Selfpos;
    public GameObject plant;
    public float speed;
    [SerializeField] float timer;
    
    float distance;
    void Start()
    {
        timer = 0;
        Selfpos = new Vector2(Random.Range(10,20) , Random.Range(10,20));
        transform.position = Selfpos;
    }

    // Update is called once per frame
    void Update()
    {
        if (plant.gameObject.active == true)
        {
            //timer += Time.deltaTime;
            distance = Vector2.Distance(transform.position, plant.transform.position);
            Vector2 direction = plant.transform.position - transform.position;

            //Go to Plant
            transform.position = Vector2.MoveTowards(this.transform.position, plant.transform.position, speed * Time.deltaTime);

        }

        else if(plant.gameObject.active == false)
        {
                //Go Back
                transform.position = Vector2.MoveTowards(this.transform.position, Selfpos, speed * Time.deltaTime);
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
}
