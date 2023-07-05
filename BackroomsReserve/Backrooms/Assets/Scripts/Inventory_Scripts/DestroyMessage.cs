using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMessage : MonoBehaviour
{
    public float timeToDestroy = 2f;

    void Update()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
