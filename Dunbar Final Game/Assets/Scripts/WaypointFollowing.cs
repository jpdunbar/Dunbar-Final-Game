using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointFollowing : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public int currentWaypoint;
    public WaypointFollowing script;
    int m_CurrentWaypointIndex;
    int reset;
    int place;
    int ok;
    public int numberCheckpoints;
    public int mayber;
    float speed;
    bool check;
    bool start;
    float distance;
    //Thinking about getting a reference of each racer
    //This will allow me to track score, position, maybe give them a new starting position?
    //Distance is used for distance to next waypoint in case they are tied
    //Will use greater than, not greater than equal to for next position in race

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
        currentWaypoint = 7;
        numberCheckpoints = 0;
        distance = 0;
        reset = 7;
        place = 1;
        distance = 0;
        check = false;
        start = true;
        speed = Random.Range(10.0f, 12.0f);
        GetComponent<NavMeshAgent>().angularSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (reset == waypoints.Length)
        {
            reset = 0;
            currentWaypoint = 0;
        }

        if (check == false)
        {
            m_CurrentWaypointIndex = currentWaypoint;
            check = true;
        }

        if(start == false)
        {
            GetComponent<NavMeshAgent>().angularSpeed = 120;
            GetComponent<NavMeshAgent>().speed = speed;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            numberCheckpoints += 1;
            start = false;
            reset += 1;
            currentWaypoint += 1;
            check = false;
            speed = Random.Range(10.0f, 12.0f);
        }
    }
}
