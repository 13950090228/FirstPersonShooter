using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 解锁关卡
/// </summary>

public class Unlock : MonoBehaviour 
{
    public GameObject checkPoint;
    private EnemySpawn spawn;


    private void Start()
    {
        spawn = GetComponent<EnemySpawn>();
    }


    private void Update()
    {
        if(spawn.maxCount == spawn.spawnedCount)
        {
            checkPoint.SetActive(true);
        }
    }
}
