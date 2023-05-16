using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    //Menu 
    [Header("MenuASSET")]
    [SerializeField] private Animator AnimatorOfCamera;
    private bool Trigger;
    [SerializeField] private GameObject Txt;
    [SerializeField] private GameObject TxtMenu;
    void Update()
    {
        //MenuAnyButton
        if (Input.anyKeyDown) Trigger = true;
        if (Trigger == true)
        {
            AnimatorOfCamera.SetTrigger("CamToMoitor");
            Txt.SetActive(false);
            TxtMenu.SetActive(true);
        }
           

        //End
    }


}
