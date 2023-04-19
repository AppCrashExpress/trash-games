using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : ControllerBase
{
    public string m_Axis;

    public override float GetMovement()
    {
        return Input.GetAxisRaw(m_Axis);
    }
}
