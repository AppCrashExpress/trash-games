using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakeReaction : MonoBehaviour
{

    private Material flakeMaterial;
    private Rigidbody rigidBody;
    private readonly Color markedColor  = new Color(0.0f, 0.0f, 1.0f);
    private readonly Color defaultColor = new Color(1.0f, 1.0f, 1.0f);
    // Start is called before the first frame update
    void Start()
    {
        flakeMaterial = GetComponent<Renderer>().material;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushMarked(Vector3 force, ForceMode mode = ForceMode.Impulse)
    {
        if (tag != "MarkedFlake")
        {
            return;
        }

        rigidBody.AddForce(force, mode);        
    }

    public void MarkFlake()
    {
        tag = "MarkedFlake";
        flakeMaterial.color = markedColor;
    }

    public void UnmarkFlake()
    {
        tag = "Flake";
        flakeMaterial.color = defaultColor;
    }
}
