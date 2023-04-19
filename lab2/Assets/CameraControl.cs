using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    
    public float upperSpeed = 5.0f;
    public float mouseXSen = 1.5f;
    public float mouseYSen = 1.5f;
    public float defaultDistance = 1.0f;

    private float mouseXRot = 0.0f;
    private float mouseYRot = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical   = Input.GetAxis("Vertical");
        float upDown     = Input.GetAxis("UpDown");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        transform.Translate(moveDirection * upperSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * upDown * upperSpeed * Time.deltaTime, Space.World);

        float mouseX = Input.GetAxis("Mouse X") * mouseXSen;
        float mouseY = Input.GetAxis("Mouse Y") * mouseYSen;

        mouseXRot += mouseX;
        mouseYRot -= mouseY;

        mouseXRot = Mathf.Repeat(mouseXRot, 360.0f);
        mouseYRot = Mathf.Clamp(mouseYRot, -90.0f, 90.0f);

        transform.localEulerAngles = Vector3.right * mouseYRot + Vector3.up * mouseXRot;
    }
}
