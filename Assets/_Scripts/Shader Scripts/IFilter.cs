using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFilter
{
    public void ActivateFilter();
    public void DeactivateFilter();
    public void UpdateFilterParam(float value);
}
