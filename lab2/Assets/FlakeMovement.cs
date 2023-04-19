using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakeMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;

    private Dictionary<string, Vector3> keyForceMappings;
    private Rigidbody rigidBody;
    private Collider[] zonesColliders;

    // Start is called before the first frame update
    void Start()
    {
        keyForceMappings = new Dictionary<string, Vector3>
        {
            { "[8]", new Vector3( 0,  0,  1) },
            { "[2]", new Vector3( 0,  0, -1) },
            { "[6]", new Vector3( 1,  0,  0) },
            { "[4]", new Vector3(-1,  0,  0) },
            { "[5]", new Vector3( 0,  1,  0) },
            { "[0]", new Vector3( 0, -1,  0) },
        };

        rigidBody = GetComponent<Rigidbody>();
        zonesColliders = GameObject.FindGameObjectsWithTag("TriggerZone")
            .Select(obj => obj.GetComponent<Collider>()).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<string, Vector3> entry in keyForceMappings)
        {
            if (Input.GetKey(entry.Key))
            {
                Vector3 newPos = transform.position + entry.Value * movementSpeed * Time.deltaTime;
                if (zonesColliders.Any(e => e.bounds.Contains(newPos)))
                {
                    transform.position = newPos;
                }
            }
        }
    }
}
