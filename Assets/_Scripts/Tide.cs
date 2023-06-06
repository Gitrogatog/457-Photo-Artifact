using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tide : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0005f * Vector3.up * Mathf.Sin(Time.realtimeSinceStartup));
    }
}
