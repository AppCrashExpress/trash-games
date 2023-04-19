using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField]
    float m_MoveSpeed;

    private ControllerBase m_Controller;
    private Rigidbody2D m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Controller = GetComponent<ControllerBase>();        
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float dir = m_Controller.GetMovement();
        m_Rigidbody.MovePosition(transform.position + Vector3.up * dir * Time.fixedDeltaTime * m_MoveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
