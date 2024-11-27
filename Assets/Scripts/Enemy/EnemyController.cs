using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("NavMesh")]
    public NavMeshAgent navMeshAgent;
    public Transform target;
    public Terrain terrain;

    [Header("Crystals")]
    List<GameObject> crystals = new List<GameObject>();
    public int activeCrystals = 0;

    [Header("Enemy")]
    public int hp = 10;


    // Start is called before the first frame update
    void Start()
    {
        //creates a list of all six crystal childs
        foreach (Transform child in transform)
        {
            crystals.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position;
        targetPosition.y = terrain.SampleHeight(targetPosition); // Match target to terrain height(suggestion from chat when i couldn't make it work, made it work but didn't dare removing it...)
        navMeshAgent.SetDestination(targetPosition);

        DebugPath(navMeshAgent.destination);

        //checks if all crystals are destroyed and updates them
        if (activeCrystals == 0) updateCrystals();

        //destroys the enemy when dead
        if (hp <= 0) Destroy(gameObject);
    }

    void DebugPath(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(targetPosition, path);

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 0.1f);
        }
    }

    void updateCrystals()
    {
        //gets 2 random crystals to reactivate
        int crystalIndex1 = Random.Range(0, crystals.Count - 1);
        int crystalIndex2 = Random.Range(0, crystals.Count-1);
        while (crystalIndex2 == crystalIndex1) crystalIndex2 = Random.Range(0, crystals.Count-1); //to ensure the random aren't the same

        //activates the selected crystals
        for (int i = 0;i < crystals.Count; i++)
        {
            if (i == crystalIndex1 || i == crystalIndex2)
            {
                crystals[i].SetActive(true);
                activeCrystals++;
            }
        }
    }
}
