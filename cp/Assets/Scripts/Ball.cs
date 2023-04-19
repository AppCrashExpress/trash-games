using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float m_MoveSpeed;

    private AudioSource m_AudioSource;
    private Rigidbody2D m_Rigidbody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_AudioSource.Play();
    }

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Rigidbody.velocity = new Vector2(1, 1) * m_MoveSpeed;
    }
}
