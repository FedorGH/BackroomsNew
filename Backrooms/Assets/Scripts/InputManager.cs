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
    public GameObject Monitor;
    [Header("Ok")]
    public GameObject playbutton;
    void Update()
    {
        //MenuAnyButton
        if (Input.anyKeyDown) Trigger = true;
        if (Trigger == true)
        {
            AnimatorOfCamera.SetTrigger("CamToMoitor");
            Txt.SetActive(false);
            StartCoroutine(changevideo());
        }
         
    }
    public IEnumerator changevideo()
    {
        var VideoPlayer = playbutton.GetComponent<UnityEngine.Video.VideoPlayer>();
        VideoPlayer.url = "https://youtu.be/N5e4WqWx5Bk";
        yield return new WaitForSeconds(20f);
        TxtMenu.SetActive(true);

    }
}
