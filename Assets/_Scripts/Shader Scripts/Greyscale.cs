using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greyscale : MonoBehaviour, IFilter
{
    public Material _mat;
    public void ActivateFilter(){
        _mat.SetFloat("_Saturation", 1);
    }
    public void DeactivateFilter(){
        _mat.SetFloat("_Saturation", 1);
    }
    public void UpdateFilterParam(float value){
        _mat.SetFloat("_Saturation", Mathf.Clamp01(value));
    }
}
