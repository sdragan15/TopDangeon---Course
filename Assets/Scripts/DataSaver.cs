using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataSaver
{
    public int[] times;
    public string[] names;

    public DataSaver(Record[] records)
    {
        times = new int[15];
        names = new string[15];

        for(int i=0; i<15; i++)
        {
            times[i] = records[i].time;
            names[i] = records[i].name;
        }
    }
}
