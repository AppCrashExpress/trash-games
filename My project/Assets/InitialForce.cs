using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialForce : MonoBehaviour
{
    public Vector3 impulse = new Vector3(5.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(impulse, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
