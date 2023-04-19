using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusFill : MonoBehaviour
{
    public GameObject[] m_Prefabs;

    [SerializeField]
    private float m_UnitRadius;
    [SerializeField]
    private float m_InnerUnitRadius;
    [SerializeField]
    private int spawnCount;
    [SerializeField]
    private int spawnVariance;

    private float m_Radius;
    private float m_InnerRadius;

    void Start()
    {
        m_Radius = transform.localScale.x * m_UnitRadius;
        m_InnerRadius = transform.localScale.x * m_InnerUnitRadius;

        FillTorus();
    }

    void FillTorus()
    {
        for (float t = 0.0f; t < 360.0f; t += 18.0f)
        {
            Vector3 sliceCentre = new Vector3(Mathf.Sin(t) * m_Radius, 0, Mathf.Cos(t) * m_Radius);
            SpawnInSlice(sliceCentre, t, spawnCount + Random.Range(-spawnVariance, spawnVariance+1));
        }
    }

    void SpawnInSlice(Vector3 pos, float angle, int count)
    {
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.up);
        for (int i = 0; i < count; ++i)
        {
            Vector3 randCircle = Random.insideUnitCircle * m_InnerRadius;
            randCircle = quat * randCircle;

            Vector3 spawnPos = pos + randCircle;
            CreateRandomPrefabAt(spawnPos);
        }
    }

    void CreateRandomPrefabAt(Vector3 pos)
    {
        int i = Random.Range(0, m_Prefabs.Length);
        Instantiate(m_Prefabs[i], pos, Quaternion.identity);
    }
}
