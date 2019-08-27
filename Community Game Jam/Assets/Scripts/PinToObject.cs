using UnityEngine;

public class PinToObject : MonoBehaviour
{
    public Transform ObjectToPinTo;

    void LateUpdate()
    {
        transform.position = ObjectToPinTo.position;
    }
}
