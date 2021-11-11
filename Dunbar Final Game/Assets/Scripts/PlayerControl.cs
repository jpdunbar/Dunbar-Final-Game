using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector3 movement;
    public Vector3 rotation;
    private Rigidbody rb;
    public GameObject car;
    public float speed = 20f;
    public float turnSpeed = 20f;
    public int opponentCheckpoints;
    public int playerCheckpoints;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        opponentCheckpoints = car.GetComponent<WaypointFollowing>().numberCheckpoints;
        Debug.Log(opponentCheckpoints);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(0f, 0f, vertical);
        rotation = new Vector3(0f, horizontal, 0f); ;
        transform.Translate(movement * Time.deltaTime * speed);
        transform.Rotate(rotation * Time.deltaTime * turnSpeed);
        movement.Normalize();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            playerCheckpoints += 1;
        }
    }
}
