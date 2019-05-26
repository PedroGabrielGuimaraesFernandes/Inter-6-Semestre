using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManagger : MonoBehaviour
{
    public GameObject[] totalWayPoints;

    public int[] IndexWay1;
    public int dimensionWay1;
    public int[] IndexWay2;
    public int dimensionWay2;
    public SpawnControl Spawn;
    [HideInInspector] public int SpawnPoint;
    
    public void Update()
    {
        if (Spawn.CurrentWave > 0 && Spawn.CurrentWave < Spawn.waves.Length) {
            if (Spawn.waves[Spawn.CurrentWave].useSpawnPoint1)
            {
                SpawnPoint = 2;
            }
            else
            {
                SpawnPoint = 1;
            }
        }
    }
}
