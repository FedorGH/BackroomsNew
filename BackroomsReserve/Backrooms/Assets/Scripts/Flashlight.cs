using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlightLight;
    public float fadeSpeed = 0.5f;
    public float maxIntensity = 1f;
    public float minIntensity = 0f;
    private bool isBatteryPickedUp = false;
    public float maxRaycastDistance;
    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRaycastDistance))
            {
               if(hit.collider.CompareTag("Battery"))
                {
                    isBatteryPickedUp = true;
                    Destroy(hit.transform.gameObject); // ”ничтожаем подобранную батарейку
                }
            }
        }
        Mathf.Clamp(flashlightLight.intensity, 0, 2);
        if (isBatteryPickedUp)
        {
            PlusMinusIntensity();
            isBatteryPickedUp = false;
        }
        else
        {
            MinusIntensity();
        }

        if (Input.GetMouseButtonDown(0))
        {
            flashlightLight.enabled = !flashlightLight.enabled;
        }
    }

    private void PlusMinusIntensity()
    {
        flashlightLight.intensity += maxIntensity;
    }


    private void MinusIntensity()
    {
        float currentIntensity = flashlightLight.intensity;
        float newIntensity = Mathf.Lerp(currentIntensity, minIntensity, fadeSpeed * Time.deltaTime);
        flashlightLight.intensity = newIntensity;
    }
}
