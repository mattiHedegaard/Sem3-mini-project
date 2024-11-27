using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public float spawnCooldown;
    float spawnTimer;
    public Transform spawnPos;
    public GameObject enemyPrefab;

    [Header("For Enemy")]
    public Terrain terrain;
    public Transform target;

    private void Start()
    {
        //sets the timer so the player has a few seconds before enemies spawn
        spawnTimer = 10 + Random.Range(-5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer > 0) spawnTimer -= 1 * Time.deltaTime;

        //spawns the enemy and sets it target for the navmesh
        if (spawnTimer <= 0)
        {
            GameObject enemy = Instantiate(enemyPrefab,transform.position,transform.rotation);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            spawnTimer = spawnCooldown + Random.Range(-5f,5f);
            enemyController.target = target;
            enemyController.terrain = terrain;
        }
    }
}
