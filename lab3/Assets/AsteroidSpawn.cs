using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public GameObject[] m_AsteroidPrefabs;
    public float m_ForceStrength;
    public float m_ForceRandom;

    private Bounds m_ColliderBounds;

    // Start is called before the first frame update
    void Start()
    {
        m_ColliderBounds = GetComponent<Collider>().bounds;
        StartCoroutine(SpawnAsteroids());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            SpawnOneAsteroid();
            yield return new WaitForSeconds(1.0f);
        }
    }

    void SpawnOneAsteroid()
    {
        Vector3 point = GetRandomPointInBounds(m_ColliderBounds);
        Vector3 endPoint = GetRandomPointInBounds(m_ColliderBounds);
        endPoint.z = 0;

        Vector3 forceVector = (endPoint - point).normalized;
        
        GameObject ast_type =  m_AsteroidPrefabs[Random.Range(0, m_AsteroidPrefabs.Length)];
        GameObject ast = Instantiate(ast_type, point, Quaternion.identity);

        float randForce = Random.Range(-m_ForceRandom, m_ForceRandom);
        ast.GetComponent<Rigidbody>().AddForce(forceVector * (m_ForceStrength + randForce), ForceMode.VelocityChange);

    }

    Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
