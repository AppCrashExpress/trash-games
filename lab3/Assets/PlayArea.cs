using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayArea : MonoBehaviour
{
    private Transform m_PlayerTransform;
    private Collider m_Collider;
    void Start()
    {
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_Collider = GetComponent<Collider>();
    }

    void Update()
    {
        Vector3 playerPos = m_PlayerTransform.position;
        if (!m_Collider.bounds.Contains(playerPos)) {
            GameObject[] objects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var obj in objects)
            {
                if (obj.CompareTag("Zone"))
                    continue;
                obj.transform.position -= playerPos;
            }
        }
    }
}
