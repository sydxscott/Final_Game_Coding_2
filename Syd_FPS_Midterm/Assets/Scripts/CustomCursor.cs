using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{

    private void Awake()
    {
        transform.position = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        //have custom cursor follow the mouse around 
        transform.position = Input.mousePosition;

    }
}
