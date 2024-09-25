using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform focus;
    Vector3 offset = new Vector3(4, 0, 4);
    float rotationSpeed = .5f;
    Quaternion angle;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = focus.position + offset;
        angle = Quaternion.Euler(0, (Input.GetAxis("Mouse X") * rotationSpeed), 0);
        offset = angle * offset;
        transform.LookAt(focus);
    }
}