using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public Transform[] waypoints;

    public Vector3 movement;
    public Vector3 rotation;

    private Rigidbody rb;

    public GameObject Opponent;
    public GameObject Opponent2;
    public GameObject Opponent3;
    public GameObject Opponent4;
    public GameObject Opponent5;

    public TextMeshProUGUI currentPlace;
    public TextMeshProUGUI currentLap;

    private float speed = 12f;
    private float turnSpeed = 25f;

    private int opponentCheckpoints;
    private int opponent2Checkpoints;
    private int opponent3Checkpoints;
    private int opponent4Checkpoints;
    private int opponent5Checkpoints;

    private float opponentdistance;
    private float opponent2distance;
    private float opponent3distance;
    private float opponent4distance;
    private float opponent5distance;

    private int playerCheckpoints;
    private int playerPosition;
    private float playerDistance;

    private int index;
    private int currentWaypoint;
    private int reset;
    private bool check;
    private string[] checkpoints;
    private int currentCheckpoint;
    private int currentLapNumber;

    private int[] allOpponentPositions = new int[5];
    private float[] allOpponentDistances = new float[5];

    void Start()
    {
        checkpoints = new string[] { "Checkpoint", "Checkpoint2", "Checkpoint3", "Checkpoint4", "Checkpoint5", "Checkpoint6", "Checkpoint7", "Checkpoint8" };
        rb = GetComponent<Rigidbody>();
        playerCheckpoints = 0;
        reset = 7;
        currentWaypoint = 7;
        check = false;
        currentLapNumber = 1;
    }

    void Update()
    {
        if (reset == waypoints.Length)
        {
            reset = 0;
            currentWaypoint = 0;
        }

        if (currentCheckpoint == 8)
        {
            currentCheckpoint = 0;
            currentLapNumber += 1;
            currentLap.text = "Current Lap: " + currentLapNumber.ToString();
        }

        if (check == false)
        {
            index = currentWaypoint;
            check = true;
        }

        opponentCheckpoints = Opponent.GetComponent<WaypointFollowing>().numberCheckpoints;
        opponent2Checkpoints = Opponent2.GetComponent<WaypointFollowing>().numberCheckpoints;
        opponent3Checkpoints = Opponent3.GetComponent<WaypointFollowing>().numberCheckpoints;
        opponent4Checkpoints = Opponent4.GetComponent<WaypointFollowing>().numberCheckpoints;
        opponent5Checkpoints = Opponent5.GetComponent<WaypointFollowing>().numberCheckpoints;

        opponentdistance = Opponent.GetComponent<WaypointFollowing>().distance;
        opponent2distance = Opponent2.GetComponent<WaypointFollowing>().distance;
        opponent3distance = Opponent3.GetComponent<WaypointFollowing>().distance;
        opponent4distance = Opponent4.GetComponent<WaypointFollowing>().distance;
        opponent5distance = Opponent5.GetComponent<WaypointFollowing>().distance;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(0f, 0f, vertical);
        rotation = new Vector3(0f, horizontal, 0f); ;
        transform.Translate(movement * Time.deltaTime * speed);
        transform.Rotate(rotation * Time.deltaTime * turnSpeed);
        movement.Normalize();

        playerDistance = Vector3.Distance(waypoints[index].position, transform.position);

        allOpponentPositions[0] = opponentCheckpoints;
        allOpponentPositions[1] = opponent2Checkpoints;
        allOpponentPositions[2] = opponent3Checkpoints;
        allOpponentPositions[3] = opponent4Checkpoints;
        allOpponentPositions[4] = opponent5Checkpoints;

        allOpponentDistances[0] = opponentdistance;
        allOpponentDistances[1] = opponent2distance;
        allOpponentDistances[2] = opponent3distance;
        allOpponentDistances[3] = opponent4distance;
        allOpponentDistances[4] = opponent5distance;

        determinePosition(allOpponentPositions, allOpponentDistances);
    }

    void determinePosition(int[] places, float[] distances)
    {
        playerPosition = 1;

        for (int i = 0; i < places.Length; i++)
        {
            if (playerCheckpoints <= places[i])
            {
                if (playerDistance > distances[i])
                {
                    playerPosition += 1;
                }
            }
        }
        currentPlace.text = "Current Place: " + playerPosition.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(checkpoints[currentCheckpoint]))
        {
            currentCheckpoint += 1;
            playerCheckpoints += 1;
            reset += 1;
            currentWaypoint += 1;
            check = false;
        }
    }
}
