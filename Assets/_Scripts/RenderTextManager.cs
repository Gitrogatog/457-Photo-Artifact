using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextManager : MonoBehaviour
{
    [SerializeField] private RenderTexture texture;
    // Start is called before the first frame update
    void Awake()
    {
        texture.height = Screen.height;
        texture.width = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
