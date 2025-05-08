using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed = 10;
    Ray ray;
    RaycastHit hit;

    public ParticleSystem ps;

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButton(0))
            {
                RotateMe();
                ps.Emit(1);
            }
        }
    }

    public void RotateMe()
    {
        float rotX = Input.GetAxis("Mouse X") * rotateSpeed * Mathf.Deg2Rad;
        transform.RotateAround(Vector3.up, -rotX);
    }
}
