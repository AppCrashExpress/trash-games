using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRails : MonoBehaviour
{
    public float m_TorusRadius;
    public float m_Offset;

    private Transform m_PlayerTransform;
    void Start()
    {
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 pos = m_PlayerTransform.position;
        float circlePos = Mathf.Atan2(pos.x, pos.z);
        
        circlePos -= m_Offset;

        Vector3 camPos = new Vector3(Mathf.Sin(circlePos), 0, Mathf.Cos(circlePos));
        camPos *= m_TorusRadius;

        Vector3 lookAtPos = m_PlayerTransform.position;

        transform.position = camPos;
        transform.LookAt(lookAtPos);
    }
}
