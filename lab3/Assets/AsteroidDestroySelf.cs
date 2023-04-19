using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroySelf : MonoBehaviour
{
    [SerializeField]
    private float m_Distance;

    private Transform m_PlayerTransform;
    // Start is called before the first frame update
    void Start()
    {
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, m_PlayerTransform.position) > m_Distance)
        {
            Destroy(gameObject);
        }
    }
}
