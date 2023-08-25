using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SatelliteOrbit : MonoBehaviour
{
    public float SemiMajorAxis = 5f;
    public float SemiMinorAxis = 3f;
    public float OrbitSpeed = 1f;

    private float t;

    void Update()
    {
        t += Time.deltaTime * OrbitSpeed;
        Vector3 newPosition = GetPosition(t);
        transform.position = newPosition;
    }

    Vector3 GetPosition(float t)
    {
        float angle = Mathf.Deg2Rad * (t * 360f);
        float z = SemiMajorAxis * Mathf.Cos(angle);
        float y = SemiMinorAxis * Mathf.Sin(angle);

        return new Vector3(0, y, z);
    }
}

