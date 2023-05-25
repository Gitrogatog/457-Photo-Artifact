using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    public UnityEvent OnPhoto;
    //List of filters
    [SerializeField] private Material[] _filterArray;
    private int _filterIndex = 0;
    private float _currentFilterParam = 0f;
    [SerializeField] private float _paramDelta;
    // Start is called before the first frame update
    void Awake()
    {
        if(OnPhoto == null){
            OnPhoto = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //gather input
        //change filter
        float filterChange = Input.GetAxisRaw("Filter");
        if(filterChange != 0) ChangeFilter((int)Mathf.Sign(filterChange));
        //change filter parameter
        _currentFilterParam += Input.GetAxisRaw("FilterParam") * _paramDelta * Time.deltaTime;
        _currentFilterParam = Mathf.Clamp01(_currentFilterParam);
        UpdateFilterParam(_currentFilterParam);
        //take photo
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            TakePhoto();
        }
    }

    void TakePhoto(){
        OnPhoto.Invoke();
    }

    void ChangeFilter(int amount){

    }

    void UpdateFilterParam(float filterParam){

    }
}
