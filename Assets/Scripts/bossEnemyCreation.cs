using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossEnemyCreation : MonoBehaviour
{
    public float generatingTime = 7.0f;
    private float lastTime;
    public GameObject enemy;
    public int maxNumOfKids = 3;
    public List<GameObject> listOfKids;

    public void FixedUpdate()
    {
        foreach (GameObject g in listOfKids.ToArray())
        {
            //Debug.Log(g);
            if (g == null)
            {
                listOfKids.Remove(g);
            }
        }

        if (Time.time - lastTime >= generatingTime)
        {
            lastTime = Time.time;
            //Debug.Log("-----------------");
            
            if (listOfKids.Count < maxNumOfKids)
            {
                listOfKids.Add(Instantiate(enemy, new Vector3(this.transform.position.x + Random.Range(-0.25f, 0.25f), this.transform.position.y + Random.Range(-0.25f, 0.25f), 0), Quaternion.identity));
                
            }
           

        }
    }
}
