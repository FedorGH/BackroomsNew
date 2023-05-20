using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsControl : MonoBehaviour
{
    [SerializeField] private Animator STARTanim;
    [SerializeField] private Animator PressedOUT;

    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        StartCoroutine(startgames());
        Debug.Log("Click");
    }

    public void SettingsGame()
    {
        //здесь настраиваем открытие настроек
    }

    private IEnumerator startgames()
    {
        PressedOUT.SetTrigger("Fade");
        STARTanim.SetTrigger("Pressed");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Level0");
    }
}
