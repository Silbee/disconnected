using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    public virtual void EntityHit()
    {
        //Do something
        print(gameObject.name + " has been hit!");
    }
}
