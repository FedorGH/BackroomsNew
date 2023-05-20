using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastManager : MonoBehaviour
{
    public float maxRaycastDistance;
    [Header("FlashlightGRAB")]
    public GameObject Flashlight;
    public GameObject FlashlightInHands;

    [Header("ChargeIT")]
    [SerializeField] private float ChargeBUFF;
    [SerializeField] private float ChargeDEBUFF;
    [SerializeField] private float Max;


    [Header("Other")]
    public Light FlashLightPower;
    private void Update()
    {
        
        if (FlashLightPower.intensity < Max) FlashLightPower.intensity = Max;
        FlashLightPower.intensity =- ChargeDEBUFF;


        //FlashlightGrab
        if (Input.GetKey(KeyCode.F))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits this object
            if (Physics.Raycast(ray, out hit, maxRaycastDistance))
            {
                if (hit.collider.CompareTag("Flashlight"))
                {
                    FlashlightInHands.SetActive(true);
                    Flashlight.SetActive(false);
                }
            }
        }    
        
        //Battery
        if (Input.GetKey(KeyCode.F))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hig;

            // Check if the ray hits this object
            if (Physics.Raycast(ray, out hig, maxRaycastDistance))
            {
                if (hig.collider.CompareTag("battery"))
                {
                    Debug.Log("stay!");
                    FlashLightPower.intensity += ChargeBUFF;
                    Destroy(hig.transform.gameObject);
                }
            }
        }


        
    }
}
