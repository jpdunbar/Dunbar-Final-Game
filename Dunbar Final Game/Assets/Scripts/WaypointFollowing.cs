using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointFollowing : MonoBehaviour
{

    public GameObject Player;

    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public int currentWaypoint;
    int m_CurrentWaypointIndex;
    int reset;
    public int numberCheckpoints;
    float speed;
    bool check;
    bool start;
    public float distance;
    public string[] checkpoints;
    int currentCheckpoint;
    private int startRacing;
    bool passedOne;

    void Start()
    {
        checkpoints = new string[] { "Checkpoint", "Checkpoint2", "Checkpoint3", "Checkpoint4", "Checkpoint5", "Checkpoint6", "Checkpoint7", "Checkpoint8" };
        currentCheckpoint = 0;
        //navMeshAgent.SetDestination(waypoints[0].position);
        currentWaypoint = 7;
        numberCheckpoints = 0;
        distance = 0;
        reset = 7;
        check = false;
        start = true;
        passedOne = false;
        speed = Random.Range(24.0f, 27.0f);
        GetComponent<NavMeshAgent>().angularSpeed = 800;
    }

    void Update()
    {
        startRacing = Player.GetComponent<PlayerControl>().introActive;

        if(startRacing > 0 && passedOne == false)
        {
            navMeshAgent.SetDestination(waypoints[0].position);
        }

        if (reset == waypoints.Length)
        {
            reset = 0;
            currentWaypoint = 0;
        }

        if(currentCheckpoint == 8)
        {
            currentCheckpoint = 0;
        }

        if (check == false)
        {
            m_CurrentWaypointIndex = currentWaypoint;
            check = true;
        }

        if (start == false)
        {
            passedOne = true;
            GetComponent<NavMeshAgent>().angularSpeed = 800;
            GetComponent<NavMeshAgent>().speed = speed;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            distance = Vector3.Distance(waypoints[m_CurrentWaypointIndex].position, transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(checkpoints[currentCheckpoint]))
        {
            currentCheckpoint += 1;
            numberCheckpoints += 1;
            start = false;
            reset += 1;
            currentWaypoint += 1;
            check = false;
            speed = Random.Range(24.0f, 27.0f);
        }
    }
}
