using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brightness : MonoBehaviour, IFilter
{
    public Material _mat;
    public void ActivateFilter(){
        _mat.SetFloat("_Brightness", 0.5f);
    }
    public void DeactivateFilter(){
        _mat.SetFloat("_Brightness", 0);
    }
    public void UpdateFilterParam(float value){
        _mat.SetFloat("_Brightness", Mathf.Clamp(value - 0.5f, -0.5f, 0.5f));
    }
}
