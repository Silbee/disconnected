using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMarker : MonoBehaviour
{
    public Transform ObjectToTarget;
    public float TrackSpeed = 10f;
    public float TrackSize = 1f;

    Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("GameController").transform;
    }

    void Update()
    {
        Vector3 MarkerPosition = TrackObject() ? ObjectToTarget.position : playerTransform.position;
        transform.position = Vector3.Lerp(transform.position, MarkerPosition, Time.deltaTime * TrackSpeed);

        Vector3 MarkerSize = TrackObject() ? Vector3.one * TrackSize : Vector3.zero;
        transform.localScale = Vector3.Lerp(transform.localScale, MarkerSize, Time.deltaTime * TrackSpeed);
    }

    bool TrackObject()
    {
        if (!ObjectToTarget)
            return false;
        if (ObjectToTarget.gameObject.activeInHierarchy)
            return true;
        return false;
    }
}
