using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public delegate void m_ShipCrashedHandler(GameObject source, int crashCount);
    public event m_ShipCrashedHandler m_OnShipCrash;

    public float m_MouseXSen;
    public float m_MouseYSen;
    public float m_HorizontalSpeed;
    public float m_VerticalSpeed;

    public float m_MaxSpeed;

    private Rigidbody m_Rigidbody;
    private int m_CollisionCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        ++m_CollisionCount;
        m_OnShipCrash?.Invoke(gameObject, m_CollisionCount);
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * m_HorizontalSpeed;
        float vertical = Input.GetAxis("Vertical") * m_HorizontalSpeed;
        float mouseX = Input.GetAxis("Mouse X") * m_MouseXSen;
        float mouseY = Input.GetAxis("Mouse Y") * m_MouseYSen;

        horizontal = -horizontal;
        mouseY = -mouseY;

        Quaternion deltaRotation = Quaternion.Euler(Vector3.forward * horizontal + Vector3.right * mouseY + Vector3.up * mouseX);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        m_Rigidbody.AddRelativeForce(Vector3.forward * vertical, ForceMode.Acceleration);

        m_Rigidbody.velocity = Vector3.ClampMagnitude(m_Rigidbody.velocity, m_MaxSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
