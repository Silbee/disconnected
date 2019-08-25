using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float CameraSpeed = 10f;

    Transform PlayerCamera;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        PlayerCamera = GameObject.FindGameObjectWithTag("CameraRig").transform;
    }

    void Update()
    {
        Vector3 InputAxis = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1f);

        if(InputAxis != Vector3.zero)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-InputAxis), Time.deltaTime * MoveSpeed);
    }

    void FixedUpdate()
    {
        Vector3 InputAxis = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1f);

        rb.MovePosition(transform.position-InputAxis*Time.deltaTime*MoveSpeed);
        PlayerCamera.position = Vector3.Lerp(PlayerCamera.position, transform.position, Time.deltaTime * CameraSpeed);
    }
}
