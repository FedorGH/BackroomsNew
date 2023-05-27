using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MenuManager : MonoBehaviour
{
    public GameObject PressAnyBut;
    public GameObject Pressed;
    public GameObject Load;
    public GameObject Warning;
    public GameObject Version;
    
    public Animator AnimatorOfCamera;
    public Animator AnimOfSound;
    public Camera cam;
    public Animator animatorPRESSED;


    [Header("MusicManager")]
    public GameObject MusicManager; 
    public AudioClip Goodbyetoworld;
    private bool trigger;
    public AudioClip[] MenuMusics;
    public AudioClip ItsJustBurningMemories;
    public static AudioSource MenuMusic;  
    public AudioClip SixFortySeven;
    private void Start()
    {
        animatorPRESSED = GetComponent<Animator>(); 
        AnimatorOfCamera = GetComponent<Animator>();    


        PressAnyBut.SetActive(false);
        Pressed.SetActive(false);
        StartCoroutine(FadeWARN());
    }
    
    private void Update()
    {

        if (MusicManager.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            AnimatorOfCamera.SetTrigger("CamToMoitor");
            PressAnyBut.SetActive(false);
            StartCoroutine(ChangeVideo());
            MusicManager.SetActive(false);
        }

        if (Input.anyKeyDown && !Warning.activeInHierarchy)
        {
            trigger = true;
        }

        if (trigger)
        {
            AnimatorOfCamera.SetTrigger("CamToMoitor");
            PressAnyBut.SetActive(false);
            StartCoroutine(ChangeVideo());
        }
    }

    public IEnumerator ChangeVideo()
    {
        yield return new WaitForSeconds(1f);
        Pressed.SetActive(true);
        Version.SetActive(true);
        PlayVideo();
        trigger = false; 
    }

    //MusicChange
    public static void ChangeSong(AudioClip music)
    {
        MenuMusic.PlayOneShot(music);
    }

    public void HidePressed(GameObject hided)
    {
        hided.SetActive(false);
    }

    public void PlayVideo()
    {
        var videoPlayer = cam.GetComponent<VideoPlayer>();
        videoPlayer.Play();
    }

    public IEnumerator FadeWARN()
    {
        Warning.SetActive(true);
        AnimOfSound.SetTrigger("Play");
        yield return new WaitForSeconds(6f);
        PressAnyBut.SetActive(true);
        Warning.SetActive(false);
    }


    public void OnChangeButtonMusic()
    {
        animatorPRESSED.SetTrigger("FadeOUT");
        Pressed.SetActive(false);
        AnimatorOfCamera.SetTrigger("ChangeSong");
        Debug.Log("MusicChange");
    }


    //OTHER
    public void SixFortySevenSong()
    {
        ChangeSong(SixFortySeven);
    }
    public void BurningMemories()
    {
        ChangeSong(ItsJustBurningMemories);
    }
    public void GodbyeTow()
    {
        ChangeSong(Goodbyetoworld);
    }

}
