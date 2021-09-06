using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    private Animator anim;
    public Text usernameText;
    public Text time;
    public Text[] records = new Text[15];

    private Stopwatch stopwatch;


    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        Time.timeScale = 0;
        stopwatch = new Stopwatch();
    }

    private void Update()
    {
        time.text = stopwatch.Elapsed.Minutes.ToString() + ":" + stopwatch.Elapsed.Seconds.ToString() + "." + (stopwatch.Elapsed.Milliseconds/100).ToString();
    }

    public void HideStartMenu()
    {
        anim.SetTrigger("hideMenu");
        Time.timeScale = 1;
        stopwatch.Restart();

    }

    public void ShowStartMenu()
    {
        anim.SetTrigger("showMenu");
        Time.timeScale = 0;
    }

    public void UsernameInput(string username)
    {
        usernameText.text = username;
    }

    public void GameFinished()
    {
        stopwatch.Reset();
        ShowStartMenu();
    }

    public void StopStopWatch()
    {
        stopwatch.Stop();
    }

    public TimeSpan GetStopWatchTime()
    {
        return stopwatch.Elapsed;
    }
}
