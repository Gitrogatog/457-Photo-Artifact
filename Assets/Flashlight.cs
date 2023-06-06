using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light flashlight;

    // Start is called before the first frame update
    void Start()
    {

        flashlight = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) flashlight.enabled = !flashlight.enabled;
    }
}
