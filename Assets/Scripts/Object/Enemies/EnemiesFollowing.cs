using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemiesFollowing : MonoBehaviour
{
    Vector2 Selfpos;
    public GameObject plant;
    public float speed;
    float timer;
    
    float distance;
    void Start()
    {
        timer = 10;
       Selfpos = new Vector2(Random.Range(10,20) , Random.Range(10,20));
        transform.position = Selfpos;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (plant.gameObject.active)
        {
            distance = Vector2.Distance(transform.position, plant.transform.position);
            Vector2 direction = plant.transform.position - transform.position;

            transform.position = Vector2.MoveTowards(this.transform.position, plant.transform.position, speed * Time.deltaTime);
        }

        if(plant.gameObject.active == false)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Selfpos, speed * Time.deltaTime);
        }

        if(timer <= 0)
        {
            Destroy(this.gameObject);
            timer = 10;
        }
    }
}
