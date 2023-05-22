using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public GameObject PressAnyBut;
    public GameObject Pressed;
    public GameObject Load;
    public GameObject Warning;
    public GameObject Version;
    public AudioSource MenuMusic;
    public AudioClip MainTheme;
    public Animator AnimatorOfCamera;
    public Animator AnimOfSound;
    public Camera cam;

    private bool trigger;

    private void Start()
    {
        Load.SetActive(false);
        PressAnyBut.SetActive(false);
        Pressed.SetActive(false);
        StartCoroutine(FadeWARN());
    }

    private void Update()
    {
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
}
