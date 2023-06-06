using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("LOOK")]
    [SerializeField] private float _lookSensitivity = 5f;
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private bool _hideCursor = false;

    [Header("BODY")]
    [SerializeField] public CharacterController _controller;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _speedRun = 8f;
    [SerializeField] private float _gravity = 1f;
    [SerializeField] [Range(0f, 1f)] private float _horizontalDrag = 0.15f;
    [SerializeField] [Range(0f, 1f)] private float _verticalDrag = 0.01f;
    [SerializeField]
    float playerViewDistance = 10f, rotationSmoothTime = 0.05f, smoothMoveTime = .1f, hangTime = 1f;
 

    private Vector3 _movementInput;
    private Vector3 _cameraInput;

    private float _gravityFactor = 1f;
    private float _currentSpeed;
    private Vector3 _currentVelocity;

    private float rotationX, smoothPitch, smoothYaw, yawSmoothV, pitchSmoothV;

    public static Vector3 CAMERA_POSITION;
    void Awake(){
        Cursor.lockState = _hideCursor ? CursorLockMode.Locked : CursorLockMode.None;
    }
    protected void Update() {
		HandleCameraInput();
		HandleMovementInput();
		HandleRunInput();
		CAMERA_POSITION = _cameraHolder.position;
	}
	
	private void FixedUpdate() {
		_currentVelocity = _movementInput * _currentSpeed;
		ApplyAirDrag();
		ApplyGravity();
		MovePlayer();
		RotateCamera();
	}

	protected void LateUpdate() {
		if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
	}

	private void RotateCamera() {		
		_cameraHolder.transform.localRotation = Quaternion.Euler(smoothPitch, 0, 0);
		transform.rotation *= Quaternion.Euler(0, smoothYaw, 0);
	}
	
	private void HandleCameraInput() {
		_cameraInput.x = Input.GetAxis("Mouse X") * _lookSensitivity;
		_cameraInput.y = Input.GetAxis("Mouse Y") * _lookSensitivity;
		
		rotationX -= _cameraInput.y;
		rotationX = Mathf.Clamp(rotationX, -75f, 75f);

		smoothPitch = Mathf.SmoothDampAngle (smoothPitch, rotationX, ref pitchSmoothV, rotationSmoothTime);
		smoothYaw = Mathf.SmoothDampAngle (smoothYaw, _cameraInput.x, ref yawSmoothV, rotationSmoothTime);
	}
	
	private void HandleMovementInput() {
		Vector3 forward =  Vector3.ProjectOnPlane(_cameraHolder.forward, Vector3.up) * Input.GetAxisRaw("Vertical");
		Vector3 right = _cameraHolder.right * Input.GetAxisRaw("Horizontal");
		_movementInput = (forward + right).normalized;
	}
		
	private void MovePlayer() {
		Vector3 targetPos = _currentVelocity;
		_controller.SimpleMove(targetPos);
	}

	private void ApplyAirDrag() {
		Vector3 velocity = _currentVelocity;
		
		Vector3 horizontal = new Vector3(velocity.x, 0f, velocity.z);
		Vector3 vertical = new Vector3(0f, velocity.y, 0f);

		horizontal -= horizontal.normalized * (horizontal.magnitude * _horizontalDrag);
		vertical -= vertical.normalized * (vertical.magnitude * _verticalDrag);
		
		_currentVelocity = horizontal + vertical;
	}
		
		
	private void ApplyGravity() {
		_currentVelocity += Vector3.down * (_gravity * _gravityFactor);
	}

	
	private void HandleRunInput() {
		_currentSpeed = Input.GetKey(KeyCode.LeftShift) ? _speedRun : _speed;
	}
}
