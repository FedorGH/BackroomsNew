using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlightLight;
    public float fadeSpeed = 1f;
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

        if (isBatteryPickedUp)
        {
            PlusMinusIntensity(maxIntensity);
        }
        else
        {
            PlusMinusIntensity(minIntensity);
        }

        if (Input.GetMouseButtonDown(0))
        {
            flashlightLight.enabled = !flashlightLight.enabled;
        }
    }

    private void PlusMinusIntensity(float intensity)
    {
        float currentIntensity = flashlightLight.intensity;
        float newIntensity = Mathf.Lerp(currentIntensity, intensity, fadeSpeed * Time.deltaTime);
        flashlightLight.intensity = newIntensity;
    }
}
