using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnValidation : MonoBehaviour
{
    private Collider[] zonesColliders;
    // Start is called before the first frame update
    void Start()
    {
        zonesColliders = GameObject.FindGameObjectsWithTag("TriggerZone")
            .Select(obj => obj.GetComponent<Collider>()).ToArray();
    }

    public bool CanSpawn()
    {
        // Debug.Log(transform.position);
        return zonesColliders.Any(e => e.bounds.Contains(transform.position));
    }
}
