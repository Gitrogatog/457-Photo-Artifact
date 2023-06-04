using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFilter : MonoBehaviour, IFilter
{
    public Material _mat;
    public string _paramName = "_Lerp";
    public void ActivateFilter(){
        _mat.SetFloat(_paramName, 1);
    }
    public void DeactivateFilter(){
        _mat.SetFloat(_paramName, 0);
    }
    public void UpdateFilterParam(float value){
        _mat.SetFloat(_paramName, Mathf.Clamp01(value));
    }
}
