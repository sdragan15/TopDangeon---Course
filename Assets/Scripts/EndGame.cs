using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private Animator anim;

    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void TriggerEndAnimation()
    {
        anim.SetTrigger("showEnd");   
    }

    public void HideEndAnimation()
    {
        anim.SetTrigger("hideEnd");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

}
