using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private AudioClip[] _footstepSounds;
    [SerializeField] private float _volume;
    private List<AudioSource> _audioSources;
    [SerializeField] private float _distanceBtwnSteps;
    [SerializeField] private float _resetStepTime;
    private float _currentDistance;
    private float _timeWOStep;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponentInParent<CharacterController>();
        _audioSources = new List<AudioSource>();
        for(int i = 0; i < _footstepSounds.Length; i++){
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.volume = _volume;
            source.clip = _footstepSounds[i];
            _audioSources.Add(source);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float speed = _controller.velocity.magnitude * Time.deltaTime;
        if(speed > 0){
            _timeWOStep = 0;
            _currentDistance += speed;
            if(_currentDistance > _distanceBtwnSteps){
                _currentDistance -= _distanceBtwnSteps;
                PlayFootstep();
            }
        }
        else{
            _timeWOStep += Time.deltaTime;
            if(_timeWOStep > _resetStepTime){
                _currentDistance = 0;
            }
        }
    }

    void PlayFootstep(){
        int soundIndex = Random.Range(0, _audioSources.Count);
        _audioSources[soundIndex].Play();
    }
}
