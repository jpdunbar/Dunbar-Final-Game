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
    public float speed = 20f;
    public float turnSpeed = 20f;
    public int opponentCheckpoints;
    public int opponent2Checkpoints;
    public int opponent3Checkpoints;
    public int opponent4Checkpoints;
    public int opponent5Checkpoints;
    public float opponentdistance;
    public float opponent2distance;
    public float opponent3distance;
    public float opponent4distance;
    public float opponent5distance;
    public int playerCheckpoints;
    public int playerPosition;
    public float playerDistance;
    public int index;
    public int currentWaypoint;
    public int reset;
    public bool check;
    public bool behind;

    private int[] allOpponentPositions = new int[5];
    private float[] allOpponentDistances = new float[5];

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCheckpoints = 0;
        reset = 7;
        currentWaypoint = 7;
        check = false;
        behind = false;
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
            index = currentWaypoint;
            check = true;
        }

        playerPosition = 1;

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

        playerDistance = Mathf.Abs(transform.position.x - waypoints[index].position.x);

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
        for (int i = 0; i < places.Length; i++)
        {
            //Debug.Log(playerCheckpoints + " " + places[i]);
            if (playerCheckpoints <= places[i])
            {
                //playerPosition += 1;
                behind = true;
                //Debug.Log(playerDistance + " > " + distances[i] + " " + index);
                if (playerDistance < distances[i] && behind == true)
                {
                    //playerPosition -= 1;
                    //playerPosition += 1;
                }
            }
            behind = false;
        }
        currentPlace.text = "Current Place: " + playerPosition.ToString();
    }

    //Place
    //0-0 So 6th?
    //Then check if the player is behind any enemy

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            playerCheckpoints += 1;
            reset += 1;
            currentWaypoint += 1;
            check = false;
        }
    }
}
