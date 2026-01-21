using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoints;
    private List<Transform> spawnPointList = new();

    private void Awake()
    {
        spawnPointList.Clear();
        spawnPoints.GetComponentsInChildren<Transform>(spawnPointList);
    }

    private void Start()
    {
        SpawnTarget();
    }

    void SpawnTarget()
    {
        foreach(var point in spawnPointList)
        {
            if (point == spawnPoints) continue;
            ObjectPoolManager.Instance.Spawn(PoolId.Target, point.position, point.rotation);
        }
    }
}