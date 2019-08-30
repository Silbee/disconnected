using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : EntityBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 10f;
    public float CameraSpeed = 10f;

    public TargetMarker Marker;

    [Header("Lasers")]
    public float LaserOffset = 1f;
    public GameObject LaserPrefab;

    [Header("FOV Settings")]
    public float MaxRange = 15;
    public float FOVAngle = 30;

    public AudioClip ShootClip;
    [Range(0,1)]
    public float ShootVolume = 1f;

    public int MaxLasers = 10;
    ProjectileScript[] Lasers;
    int LaserIndex = 0;

    Vector3 LastRawInputAxis = Vector3.zero;

    Rigidbody rb;
    AudioSource source;

    Transform PlayerCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        PlayerCamera = GameObject.FindGameObjectWithTag("CameraRig").transform;

        //Get location of the main camera, used for spawning the pool of lasers directly above
        Vector3 CameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;

        //Array for all lasers in game
        Lasers = new ProjectileScript[MaxLasers];

        //Populate the Lasers array
        for (int i = 0; i < MaxLasers; i++)
        {
            Lasers[i] = Instantiate(LaserPrefab, CameraPosition + Vector3.up, Quaternion.identity).GetComponent<ProjectileScript>();
            Physics.IgnoreCollision(Lasers[i].GetComponent<Collider>(), GetComponent<Collider>());
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

        Transform target = GetEntityInFOV();
        Marker.ObjectToTarget = target;

        if (Input.GetKeyDown(KeyCode.Space))
            Shoot(target);
    }

    void FixedUpdate()
    {
        Vector3 InputAxis = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1f);

        rb.MovePosition(transform.position - InputAxis * Time.deltaTime * MoveSpeed);
        PlayerCamera.position = Vector3.Lerp(PlayerCamera.position, transform.position, Time.deltaTime * CameraSpeed);
    }

    void Shoot(Transform target)
    {
        //Get next laser
        ProjectileScript Laser = Lasers[LaserIndex];
        Laser.gameObject.SetActive(true);

        //Set the correct position and rotation for current laser
        Laser.transform.position = transform.position + transform.forward * LaserOffset;
        Laser.transform.rotation = Quaternion.LookRotation(transform.forward);

        Laser.Shoot(target);

        //Pew
        source.PlayOneShot(ShootClip, ShootVolume);

        //Update index to next laser
        LaserIndex++;
        if (LaserIndex >= Lasers.Length)
            LaserIndex = 0;
    }

    Transform GetEntityInFOV()
    {
        Collider[] entities = Physics.OverlapSphere(transform.position, MaxRange, LayerMask.GetMask("Entities"));
        List<Transform> entitiesInRange = new List<Transform>();

        for (int i = 0; i < entities.Length; i++)
        {
            if (entities[i].gameObject == gameObject) continue;
            Vector3 direction = entities[i].transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle < FOVAngle * 0.5f)
                entitiesInRange.Add(entities[i].transform);
        }

        Transform NearestEntity = null;

        for (int i = 0; i < entitiesInRange.Count; i++)
        {
            if (!NearestEntity) NearestEntity = entitiesInRange[i];
            if (Vector3.Distance(transform.position, entitiesInRange[i].position) < Vector3.Distance(transform.position, NearestEntity.position))
                NearestEntity = entities[i].transform;
        }

        return NearestEntity;
    }
}
