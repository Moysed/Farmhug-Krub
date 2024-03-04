using Lean.Pool;
using UnityEngine;

public class EnemiesFollowing : MonoBehaviour
{
    Vector2 Selfpos;
    GameObject plant;
    public float speed;
    public int hp;
    [SerializeField] float timer;
    [SerializeField] int leaveCheck = 0;
    float distance;

    void Start()
    {
        timer = 0;
        hp = 5;
        Selfpos = new Vector2(Random.Range(10, 20), Random.Range(10, 20));
        transform.position = Selfpos;

        //plant = GameObject.FindWithTag("Plant");
        if (plant == null)
        {
            Debug.LogError("Plant GameObject not found with tag 'Plant'");
        }
    }

    void Update()
    {
        if (leaveCheck == 2)
        {
            Debug.Log("Out of Edge");
            LeanPool.Despawn(gameObject);
            EnemySpawner.singleton.currentEnemyCount--;
            leaveCheck = 0;
        }

            plant = GameObject.FindWithTag("Plant");

            /*if(plant != null)
            {
            if (plant.activeSelf && EnemySpawner.singleton.currentEnemyCount == 0 && EnemySpawner.singleton.currentEnemyCount < EnemySpawner.singleton.maxEnemyCount)
            {
                LeanPool.Spawn(gameObject);
            }
        }*/
            
        

        // ******** Enemy Behaviour *************** 
        if (plant != null && plant.activeSelf)
        {
            distance = Vector2.Distance(transform.position, plant.transform.position);
            Vector2 direction = plant.transform.position - transform.position;

            //Go to Plant
            transform.position = Vector2.MoveTowards(transform.position, plant.transform.position, speed * Time.deltaTime);

            if (hp <= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, Selfpos, speed * Time.deltaTime);
            }
        }
        else
        {
            //Go Back
            transform.position = Vector2.MoveTowards(transform.position, Selfpos, speed * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CamEdge")
        {
            leaveCheck += 1;
        }
    }
}
