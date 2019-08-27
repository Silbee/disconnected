using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 10f;
    public float CameraSpeed = 10f;

    public float LaserOffset = 1f;
    public float LaserSpeed = 10f;
    public GameObject LaserPrefab;

    public int MaxLasers = 10;
    Rigidbody[] Lasers;
    int LaserIndex = 0;

    Vector3 LastRawInputAxis = Vector3.zero;

    Transform PlayerCamera;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerCamera = GameObject.FindGameObjectWithTag("CameraRig").transform;

        //Get location of the main camera, used for spawning the pool of lasers directly above
        Vector3 CameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;

        //Array for all lasers in game
        Lasers = new Rigidbody[MaxLasers];

        //Populate the Lasers array
        for (int i = 0; i < MaxLasers; i++)
        {
            Lasers[i] = Instantiate(LaserPrefab, CameraPosition + Vector3.up, Quaternion.identity).GetComponent<Rigidbody>();
            Lasers[i].gameObject.name = "Laser" + i;
            Lasers[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            LastRawInputAxis = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1f);

        if (LastRawInputAxis != Vector3.zero)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-LastRawInputAxis), Time.deltaTime * RotateSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }

    void FixedUpdate()
    {
        Vector3 InputAxis = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1f);

        rb.MovePosition(transform.position - InputAxis * Time.deltaTime * MoveSpeed);
        PlayerCamera.position = Vector3.Lerp(PlayerCamera.position, transform.position, Time.deltaTime * CameraSpeed);
    }

    void Shoot()
    {
        //Get next laser
        Rigidbody Laser = Lasers[LaserIndex];
        Laser.gameObject.SetActive(true);

        //Reset all forces on current laser
        Laser.velocity = Vector3.zero;

        //Set the correct position and rotation for current laser
        Laser.position = transform.position + transform.forward * LaserOffset;
        Laser.rotation = Quaternion.LookRotation(transform.forward);

        //Apply new forces on laser
        Laser.AddForce(transform.forward * LaserSpeed, ForceMode.Impulse);

        //Update index to next laser
        LaserIndex++;
        if (LaserIndex >= Lasers.Length)
            LaserIndex = 0;
    }
}
