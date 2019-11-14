using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{ 
    public float scale;

    void Update()
    {
        Time.timeScale = scale;       
    }
}
