using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    public UnityEvent OnPhoto;
    //List of filters
    // [SerializeField] private Material[] _filterArray;
    private List<IFilter> _filters;
    private int _filterIndex = 0;
    private float _currentFilterParam = 0f;
    [SerializeField] private float _paramDelta;
    [SerializeField] private KeyCode screenshotKey;
    // Start is called before the first frame update
    void Awake()
    {
        if(OnPhoto == null){
            OnPhoto = new UnityEvent();
        }
        _filters = new List<IFilter>();
        _filters.Add(new NullFilter());
        _filters.AddRange(GetComponentsInChildren<IFilter>());
        foreach(IFilter filter in _filters){
            filter.UpdateFilterParam(0f);
            filter.DeactivateFilter();
            Debug.Log("Filter spotted!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //gather input
        //change filter
        float filterChange = Input.mouseScrollDelta.y; //Input.GetAxisRaw("Filter");
        
        if(filterChange != 0) ChangeFilter((int)Mathf.Sign(filterChange));
        //change filter parameter
        _currentFilterParam += Input.GetAxisRaw("Filter") * _paramDelta * Time.deltaTime;
        Debug.Log("F: " + _currentFilterParam);
        _currentFilterParam = Mathf.Clamp01(_currentFilterParam);
        UpdateFilterParam(_currentFilterParam);
        //take photo
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            TakePhoto();
        }
    }

    void TakePhoto(){
        OnPhoto.Invoke();
    }

    void ChangeFilter(int amount){
        int oldIndex = _filterIndex;
        _filterIndex += amount;
        if(_filterIndex >= _filters.Count){
            _filterIndex = 0;
        }
        else if(_filterIndex < 0){
            _filterIndex = _filters.Count - 1;
        }
        if(oldIndex != _filterIndex){
            _filters[oldIndex].DeactivateFilter();
            _filters[_filterIndex].ActivateFilter();
            _filters[_filterIndex].UpdateFilterParam(_currentFilterParam);
        }
    }

    void UpdateFilterParam(float filterParam){
        _filters[_filterIndex].UpdateFilterParam(filterParam);
    }
}
