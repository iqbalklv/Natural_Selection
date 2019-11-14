using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasActivator : MonoBehaviour
{
    public GameObject canvas;

    void Awake()
    {
        canvas.SetActive(true);   
    }

    
}
