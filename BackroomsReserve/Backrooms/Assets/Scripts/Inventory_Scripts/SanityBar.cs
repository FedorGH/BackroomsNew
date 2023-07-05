using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        StartCoroutine(c_Diff());
    }

    public void AddSanity(int value)
    {
        GetComponent<Slider>().value += value;
    }

    IEnumerator c_Diff()
    {
        while (true)
        {
            --slider.value;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
