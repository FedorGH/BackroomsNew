using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsControl : MonoBehaviour
{
    [SerializeField] private GameObject Load;
    [SerializeField] private Animator STARTanim;
    [SerializeField] private Animator PressedOUT;

    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        Load.SetActive(true);
        StartCoroutine(startgames());
    }

    public void SettingsGame()
    {
        //здесь настраиваем открытие настроек
    }

    private IEnumerator startgames()
    {
        PressedOUT.SetTrigger("Fade");
        STARTanim.SetBool("AnimStart", true);
        yield return new WaitForSeconds(5f);
    }
}
