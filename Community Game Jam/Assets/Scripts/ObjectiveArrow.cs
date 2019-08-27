using UnityEngine;

public class ObjectiveArrow : MonoBehaviour
{
    public Transform ObjectiveDirectionTransform;

    RectTransform ArrowTransform;

    void Start()
    {
        ArrowTransform = GetComponent<RectTransform>();    
    }

    void Update()
    {
        ArrowTransform.rotation = Quaternion.Euler(0, 0, -ObjectiveDirectionTransform.eulerAngles.y);
    }
}
