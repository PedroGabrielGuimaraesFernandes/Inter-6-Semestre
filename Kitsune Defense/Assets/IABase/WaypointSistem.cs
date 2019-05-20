using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointSistem : MonoBehaviour
{
    public  GameObject[] WayPoints;
    public int RandonRange;
    int WayPointIndex;
    GameObject ManagerObj;
    WayPointManagger Manager;
    IABase IA;
    bool trigger = true;

    // Start is called before the first frame update
    void Start()
    {
        WayPointIndex = 0;
        IA = gameObject.GetComponent<IABase>();
        ManagerObj = GameObject.FindGameObjectWithTag("wayManager");
        Manager = ManagerObj.GetComponent<WayPointManagger>();

        if (Manager.SpawnPoint == 1)
        {
            WayPoints = new GameObject[Manager.dimensionWay1];
            for (int i = 0; i <= WayPoints.Length; i++)
            {
                WayPoints[i] = Manager.totalWayPoints[i + Manager.IndexWay1[0]];
            }
        }
        else if(Manager.SpawnPoint == 2)
        {
            WayPoints = new GameObject[Manager.dimensionWay2];
            for (int i = 0; i <= WayPoints.Length; i++)
            {
                WayPoints[i] = Manager.totalWayPoints[i + Manager.IndexWay2[0]];
            }
        }
    }

    public void MoveToObjective()
    {
        Vector3 newpos;
        float randonX;
        float randonY;
        
       if(trigger)
        {
            randonX = Random.Range((WayPoints[WayPointIndex].transform.position.x - RandonRange), (WayPoints[WayPointIndex].transform.position.x + RandonRange));
            randonY = Random.Range((WayPoints[WayPointIndex].transform.position.y - RandonRange), (WayPoints[WayPointIndex].transform.position.y + RandonRange));
            newpos  = new Vector3(randonX, randonY, WayPoints[WayPointIndex].transform.position.z);

            IA.NavAgent.SetDestination(newpos);        
            trigger = false;
        }
        else if (IA.NavAgent.remainingDistance <= 0.5)
        {
            trigger = true;
            WayPointIndex++;
        }   
    }
}
