using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrashCount : MonoBehaviour
{
    private TextMeshProUGUI m_TextMesh;
    void Start()
    {
        m_TextMesh = GetComponent<TextMeshProUGUI>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Spaceship>().m_OnShipCrash += UpdateText;

        m_TextMesh.text = "Crash count: 0";
    }

    void UpdateText(GameObject obj, int crashCount)
    {
        m_TextMesh.text = "Crash count: " + crashCount;
    }
}
