using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject enemy;


    private void FixedUpdate()
    {
        if(enemy == null)
        {
            Destroy(this.gameObject);
        }
    }

    
}
