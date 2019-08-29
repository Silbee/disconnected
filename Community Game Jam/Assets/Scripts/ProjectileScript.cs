using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Transform ObjectToHome;
    public float Speed = 50f;
    public float RotateSpeed = 200f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot(Transform homing)
    {
        ObjectToHome = homing;
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * Speed;
        if (!ObjectToHome)
            return;

        rb.velocity = transform.forward * Speed;

        Vector3 direction = ObjectToHome.position - transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.forward).y;

        rb.angularVelocity = Vector3.up * rotateAmount * RotateSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
