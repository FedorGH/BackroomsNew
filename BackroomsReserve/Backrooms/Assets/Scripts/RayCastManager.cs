using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastManager : MonoBehaviour
{
    public float maxRaycastDistance;
    public GameObject Flashlight;
    public GameObject FlashlightInHands;

    private float inten;

    private void Start()
    {
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.F))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRaycastDistance))
            {
                if (hit.collider.CompareTag("Flashlight"))
                {
                    FlashlightInHands.SetActive(true);
                    Flashlight.SetActive(false);
                }
                else if (hit.collider.CompareTag("battery"))
                {
                    Debug.Log("stay!");
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}