using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public Transform ObjectTransform;

    void Update()
    {
        transform.LookAt(ObjectTransform);
    }
}
