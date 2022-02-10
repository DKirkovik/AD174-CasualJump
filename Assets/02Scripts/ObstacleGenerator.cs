using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    #region Vars
    [Header ("Generator Vars")]

    public GameObject obstacle;
    public float obstacleSpeed = -2f;
    public float obstacleLifeTime = 15f;
    public float spawnTime;
    public Vector2 rangeX;
    public Vector2 rangeY;
    private int spawnCount;
    private bool spawned = false;
    private Camera cam;



    #endregion


    void Start()
    {
        spawned = false;
        cam = Camera.main;
    }
    
    void FixedUpdate()
    {
        if (!spawned){
            spawned = true;
            StartCoroutine(SpawnCooldown());
            SpawnObst();
            
        }
    }



    void SpawnObst()
    {
        Debug.Log("Spawned");

        
        float spawnX = 1f;  //Random.Range(rangeX.x,rangeX.y);
        float spawnY = Random.Range(rangeY.x,rangeY.y);

        Vector3 normSpawnPos = new Vector3(spawnX,spawnY,0f);

        Vector3 spawnPos = cam.ViewportToWorldPoint(normSpawnPos);


        GameObject spawnedObst = Instantiate(obstacle,spawnPos,Quaternion.identity);

        spawnedObst.transform.parent = this.gameObject.transform;
        spawnedObst.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        spawnedObst.GetComponent<Rigidbody2D>().velocity = new Vector2(obstacleSpeed, 0f);
        Destroy(spawnedObst, obstacleLifeTime);
        
            
            

    }


    IEnumerator SpawnCooldown()
    {
        //Cooldown timer
        yield return new WaitForSeconds(spawnTime);
        spawned = false;
    }
}
