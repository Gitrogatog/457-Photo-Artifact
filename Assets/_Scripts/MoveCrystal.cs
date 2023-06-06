using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrystal : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, Time.deltaTime * 10.0f);
        transform.Translate(0.001f * Vector3.forward * Mathf.Sin(Time.realtimeSinceStartup));
    }
}
