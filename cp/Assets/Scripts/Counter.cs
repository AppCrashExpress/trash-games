using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    private TextMeshProUGUI m_GuiElement;
    void Start()
    {
        m_GuiElement = GetComponent<TextMeshProUGUI>();
    }

    public void SetCount(int count)
    {
        m_GuiElement.text = count.ToString();
    }
}
