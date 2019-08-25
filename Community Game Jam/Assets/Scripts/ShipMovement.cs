using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float CameraSpeed = 10f;

    Vector3 OriginalSize;
    public Vector3 ChargingSize = Vector3.one;
    public float ChargingSpeed = 5f;
    public float ReleaseSpeed = 20f;

    Vector3 LastInputAxis = Vector3.forward;

    Transform PlayerCamera;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        OriginalSize = transform.localScale;

        PlayerCamera = GameObject.FindGameObjectWithTag("CameraRig").transform;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            LastInputAxis = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1f);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-LastInputAxis), Time.deltaTime * MoveSpeed);

        if (Input.GetKey(KeyCode.Space))
            transform.localScale = Vector3.Lerp(transform.localScale, ChargingSize, Time.deltaTime * ChargingSpeed);
        else
            transform.localScale = Vector3.Lerp(transform.localScale, OriginalSize, Time.deltaTime * ReleaseSpeed);
    }

    void Shoot()
    {

    }

    void FixedUpdate()
    {
        Vector3 InputAxis = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1f);
        rb.MovePosition(transform.position-InputAxis*Time.deltaTime*MoveSpeed);

        PlayerCamera.position = Vector3.Lerp(PlayerCamera.position, transform.position, Time.deltaTime * CameraSpeed);
    }
}
