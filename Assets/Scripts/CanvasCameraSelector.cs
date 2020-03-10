using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCameraSelector : MonoBehaviour
{
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        canvas.worldCamera = GameObject.FindWithTag("MapCam").GetComponent<Camera>();
    }
}
