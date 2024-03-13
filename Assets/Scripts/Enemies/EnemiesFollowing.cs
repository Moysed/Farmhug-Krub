using Lean.Pool;
using UnityEngine;

public class EnemiesFollowing : MonoBehaviour
{
    public GameObject plant;

    bool Isend_ = true;
    public float speed;
    public int hp;
    [SerializeField] float timer;
    [SerializeField] int leaveCheck = 0;
    float distance;
    public float floatingRadius = 200; // Radius within which the object will float
    private Vector2 Selfpos;
    private Vector2 initialPosition;
    Vector3 spawnPosition;
    float side;
    void Start()
    {
        
        timer = 0;
        hp = 1;
        Selfpos = new Vector2(Random.Range(10, 20), Random.Range(10, 20));
        transform.position = Selfpos;

        //plant = GameObject.FindWithTag("Plant");
        

        float side = Random.Range(0f, 1f); // Random value to determine side of the screen
    }

   

    void Update()
    {

        if (Isend_)
        {
            
            plant = GameObject.FindGameObjectWithTag("Plant");
            Isend_ = false;
        }

        //Debug.Log(plant);

        if (leaveCheck >= 2)
        {
            Debug.Log("Out of Edge");
            LeanPool.Despawn(gameObject);
            EnemySpawner.singleton.currentEnemyCount--;
            leaveCheck = 0;
            hp = 1;
                Isend_ = true;
        }

        
        /*if(plant != null)
        {
        if (plant.activeSelf && EnemySpawner.singleton.currentEnemyCount == 0 && EnemySpawner.singleton.currentEnemyCount < EnemySpawner.singleton.maxEnemyCount)
        {
            LeanPool.Spawn(gameObject);
        }
    }*/


        if (hp <= 0 )
        {
            plant = null;
            transform.position = Vector2.MoveTowards(transform.position, spawnPosition, speed * Time.deltaTime);
        }
       

        // ******** Enemy Behaviour *************** 
        if (plant != null && plant.activeSelf )
        {
            
            distance = Vector2.Distance(transform.position, plant.transform.position);
            Vector2 direction = plant.transform.position - transform.position;

            //Go to Plant
            transform.position = Vector2.MoveTowards(transform.position, plant.transform.position, speed * Time.deltaTime);

            RandPos();
        }
        if( plant != null && plant.active == false)
        {
            RandPos();
            transform.position = Vector2.MoveTowards(transform.position, spawnPosition, speed * Time.deltaTime);
        }

       
        /*else
        {
            Selfpos = new Vector2(Random.Range(0, 5), Random.Range(0, 5));

            // Go Back
            transform.position = Vector2.MoveTowards(transform.position, Selfpos, speed * Time.deltaTime);

        }
    }*/
    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CamEdge")
        {
            leaveCheck = 2;
        }

       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Scarecrow")
        {
            hp--;
            Debug.Log(hp);
        }
    }

    void RandPos()
    {
        Camera mainCamera = Camera.main;

        // Define spawn position outside the screen based on camera viewport
        float spawnX = Random.Range(0f, 1f); // Random x position within the viewport
        float spawnY = Random.Range(0f, 1f); // Random y position within the viewport

        // Set spawn position based on random values
         spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(spawnX, spawnY, mainCamera.nearClipPlane));

        // Determine which side of the screen to spawn the enemy

        if (side < 0.25f) // Spawn on the left side
        {
            spawnPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(-0.5f, spawnY, mainCamera.nearClipPlane)).x;
        }
        else if (side < 0.5f) // Spawn on the right side
        {
            spawnPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(1.5f, spawnY, mainCamera.nearClipPlane)).x;
        }
        else if (side < 0.75f) // Spawn on the top side
        {
            spawnPosition.y = mainCamera.ViewportToWorldPoint(new Vector3(spawnX, 1.5f, mainCamera.nearClipPlane)).y;
        }
        else // Spawn on the bottom side
        {
            spawnPosition.y = mainCamera.ViewportToWorldPoint(new Vector3(spawnX, -0.5f, mainCamera.nearClipPlane)).y;
        }

    }
}
