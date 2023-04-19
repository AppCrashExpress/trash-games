using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactCount : MonoBehaviour
{
    [SerializeField]
    private GameObject m_GuiText;

    private Counter m_Counter;
    private GameObject m_Ball;
    private int m_CollideCount = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != m_Ball)
            return;

        m_Counter.SetCount(++m_CollideCount);
    }

    void Start()
    {
        m_Ball = GameObject.FindGameObjectWithTag("Ball");
        m_Counter = m_GuiText.GetComponent<Counter>();
    }
}
