using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobinaRotate : MonoBehaviour
{
    public float Speed;
    public GameObject Bobina1;
    public GameObject Bobina2;
    
    void Update()
    {
        Bobina1.transform.Rotate(0, Speed * Time.deltaTime, 0);
        Bobina2.transform.Rotate(0, 20 * Time.deltaTime, 0);
    }
}
