using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxScroller : MonoBehaviour
{
    float rotateSpeed;
    void Start()
    {
        rotateSpeed = 1.2f;
    }

    void Update()
    {
        RenderSettings.skybox.SetFloat ("_Rotation", Time.time * rotateSpeed);
    }
}
