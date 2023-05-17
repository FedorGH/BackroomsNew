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
    [Header("Ok")]
    public Camera cam;
    [Header("WARNING")]
    public GameObject WARN;
    public AudioSource SOURCEE;
    public AudioClip WarnSound;
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
       yield return new WaitForSeconds(23f);
        TxtMenu.SetActive(true);

    }
    
    public void VIDEO()
    {
        var VideoPlayer = cam.GetComponent<UnityEngine.Video.VideoPlayer>();
        VideoPlayer.Play();
    }

    public IEnumerator FadeWARN()
    {
        SOURCEE.PlayOneShot(WarnSound);
        yield return new WaitForSeconds(6f);
        SOURCEE.Stop();
        Txt.SetActive(true);
        WARN.SetActive(false);
        GAMEMENUMUSIC.PlayOneShot(MainTheme);

    }
}
