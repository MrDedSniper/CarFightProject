using System;
using UnityEngine;

public class CarControls : MonoBehaviour
{

    public Rigidbody sphereRB;

    private void Start()
    {
        throw new NotImplementedException();
    }

    private void Update()
    {
        transform.position = sphereRB.transform.position;
    }


    /*private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float _horizontalInput;
    private float _verticalInput;
    private float _steerAngle;

    private float _currentStearAngle;
    private float _currentBreakForce;
    private bool _isBreaking;

    [SerializeField] private WheelCollider _frontLeftWheelCollider;
    [SerializeField] private WheelCollider _frontRightWheelCollider;
    [SerializeField] private WheelCollider _backLeftWheelCollider;
    [SerializeField] private WheelCollider _backRightWheelCollider;
    
    [SerializeField] private Transform _frontLeftWheelTransform;
    [SerializeField] private Transform _frontRightWheelTransform;
    [SerializeField] private Transform _backLeftWheelTransform;
    [SerializeField] private Transform _backRightWheelTransform;

    [SerializeField] private float _motorForce;
    [SerializeField] private float _breakForce;
    [SerializeField] private float _maxSteerAngle;
    
    private void FixedUpdate()
    {
        GetInput();
        HandlerMotor();
        HandleSteering();
        UpdateWheels();
    }
    private void GetInput()
    {
        _horizontalInput = Input.GetAxis(HORIZONTAL);
        _verticalInput = Input.GetAxis(VERTICAL);
        _isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandlerMotor()
    {
        _frontLeftWheelCollider.motorTorque = _verticalInput * _motorForce;
        _frontRightWheelCollider.motorTorque = _verticalInput * _motorForce;
        _currentBreakForce = _breakForce = _isBreaking ? _breakForce : 0f;

        if (_isBreaking)
        {
            ApplyBreaking();
        }
    }

    private void ApplyBreaking()
    {
        _frontLeftWheelCollider.brakeTorque = _currentBreakForce;
        _frontRightWheelCollider.brakeTorque = _currentBreakForce;
        _backLeftWheelCollider.brakeTorque = _currentBreakForce;
        _backRightWheelCollider.brakeTorque = _currentBreakForce;
    }

    private void HandleSteering()
    {
        _currentStearAngle = _maxSteerAngle * _horizontalInput;
        _frontLeftWheelCollider.steerAngle = _currentStearAngle;
        _frontRightWheelCollider.steerAngle = _currentStearAngle;
    }
    
    private void UpdateWheels()
    {
        UpdateSingleWheels(_frontLeftWheelCollider, _frontLeftWheelTransform);
        UpdateSingleWheels(_frontRightWheelCollider, _frontRightWheelTransform);
        UpdateSingleWheels(_backLeftWheelCollider, _backLeftWheelTransform);
        UpdateSingleWheels(_backRightWheelCollider, _backRightWheelTransform);
    }

    private void UpdateSingleWheels(WheelCollider WheelCollider, Transform WheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        
        WheelCollider.GetWorldPose(out position, out rotation);
        WheelTransform.rotation = rotation;
        WheelTransform.position = position;
    }*/
}
