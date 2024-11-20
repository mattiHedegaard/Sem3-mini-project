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


    // Start is called before the first frame update
    void Start()
    {
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
        targetPosition.y = terrain.SampleHeight(targetPosition); // Match target to terrain height
        navMeshAgent.SetDestination(targetPosition);

        DebugPath(navMeshAgent.destination);

        if (activeCrystals == 0) updateCrystals();
    }
    void DebugPath(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(targetPosition, path);

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 0.1f); // Lasts for 0.1s
        }


    }

    void updateCrystals()
    {
        int crystalIndex1 = Random.Range(0, crystals.Count - 1);
        int crystalIndex2 = Random.Range(0, crystals.Count-1);
        while (crystalIndex2 == crystalIndex1) crystalIndex2 = Random.Range(0, crystals.Count-1);

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
