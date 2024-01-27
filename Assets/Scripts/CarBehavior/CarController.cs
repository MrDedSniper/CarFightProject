using System;
using UnityEngine;
using System.Collections;

public class CarControls : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
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

    internal float _currentSpeed;
    
    private void FixedUpdate()
    {
        GetInput();
        HandlerMotor();
        HandleSteering();
        UpdateWheels();
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
        _currentSpeed = Mathf.Abs(localVelocity.z) * 3.6f;
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
        
        _currentBreakForce = _isBreaking ? _breakForce : 0f;

        if (_isBreaking)
        {
            ApplyBreaking();
        }
        
        else if (!_isBreaking)
        {
            StopBreaking();
        }
    }

    private void ApplyBreaking()
    {
        _frontLeftWheelCollider.brakeTorque = _currentBreakForce;
        _frontRightWheelCollider.brakeTorque = _currentBreakForce;
        _backLeftWheelCollider.brakeTorque = _currentBreakForce;
        _backRightWheelCollider.brakeTorque = _currentBreakForce;
    }
    
    private void StopBreaking()
    {
        _frontLeftWheelCollider.brakeTorque = 0f;
        _frontRightWheelCollider.brakeTorque = 0f;
        _backLeftWheelCollider.brakeTorque = 0f;
        _backRightWheelCollider.brakeTorque = 0f;
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
    }
}
