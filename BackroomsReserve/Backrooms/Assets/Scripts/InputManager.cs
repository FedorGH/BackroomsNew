using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
public class InputManager : MonoBehaviour
{
    //Menu 
    [Header("MenuASSET")]
    [SerializeField] private Animator AnimatorOfCamera;
    private bool Trigger;
    [SerializeField] private GameObject Txt;
    [SerializeField] private GameObject TxtMenu;
    public GameObject Monitor;
    [SerializeField] private Animator animofsound;

    [Header("Ok")]
    public Camera cam;
    [Header("WARNING")]
    private bool Trigger_2;
    public GameObject WARN;
    public AudioSource GAMEMENUMUSIC;
    public AudioClip MainTheme;
    private void Start()
    {
        Txt.SetActive(false);
        TxtMenu.SetActive(false);
        StartCoroutine(FadeWARN());
    }
    void Update()
    {
        //MenuAnyButton
        if (Input.anyKeyDown && !WARN.activeInHierarchy) Trigger = true;
        if (Trigger == true)
        {
            AnimatorOfCamera.SetTrigger("CamToMoitor");
            Txt.SetActive(false);
            StartCoroutine(changevideo());
        }
         
    }
    public IEnumerator changevideo()
    {
       yield return new WaitForSeconds(1f);
        TxtMenu.SetActive(true);

    }
    
    public void VIDEO()
    {
        var VideoPlayer = cam.GetComponent<UnityEngine.Video.VideoPlayer>();
        VideoPlayer.Play();
    }

    public IEnumerator FadeWARN()
    {
        WARN.SetActive(true);
        animofsound.SetTrigger("Play");
        yield return new WaitForSeconds(6f);
        Txt.SetActive(true);
        WARN.SetActive(false);
    }
}
