using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishGameScene : MonoBehaviour
{
    public string scene;

    private Animator anim;

    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(scene);
        anim.SetTrigger("hide");
        GameManager.instance.startMenu.ShowStartMenu();
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void ShowFinishedScene()
    {
        anim.SetTrigger("show");
        Time.timeScale = 0;
        GameManager.instance.startMenu.StopStopWatch();
        //Debug.Log("aaaaaaaaaaaalllooo");

    }
}
