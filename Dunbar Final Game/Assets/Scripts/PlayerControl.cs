using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
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
    public int playerCheckpoints;
    public int playerPosition;
    private int[] allOpponentPositions = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCheckpoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = 1;

        opponentCheckpoints = Opponent.GetComponent<WaypointFollowing>().numberCheckpoints;
        opponent2Checkpoints = Opponent2.GetComponent<WaypointFollowing>().numberCheckpoints;
        opponent3Checkpoints = Opponent3.GetComponent<WaypointFollowing>().numberCheckpoints;
        opponent4Checkpoints = Opponent4.GetComponent<WaypointFollowing>().numberCheckpoints;
        opponent5Checkpoints = Opponent5.GetComponent<WaypointFollowing>().numberCheckpoints;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(0f, 0f, vertical);
        rotation = new Vector3(0f, horizontal, 0f); ;
        transform.Translate(movement * Time.deltaTime * speed);
        transform.Rotate(rotation * Time.deltaTime * turnSpeed);
        movement.Normalize();

        allOpponentPositions[0] = opponentCheckpoints;
        allOpponentPositions[1] = opponent2Checkpoints;
        allOpponentPositions[2] = opponent3Checkpoints;
        allOpponentPositions[3] = opponent4Checkpoints;
        allOpponentPositions[4] = opponent5Checkpoints;
        determinePosition(allOpponentPositions);
    }

    void determinePosition(int[] places)
    {
        for (int i = 0; i < places.Length; i++)
        {
            if (playerCheckpoints < places[i])
            {
                playerPosition += 1;
            }
        }
        currentPlace.text = "Current Place: " + playerPosition.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            playerCheckpoints += 1;
        }
    }
}
