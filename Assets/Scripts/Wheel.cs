using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public class Wheel : MonoBehaviour
{
    public Action<bool> Test;
    public Action CountSpin;
    public Action<bool> RotationStopEvent;
    [SerializeField] private Button _button;
    [SerializeField] private Rigidbody2D _rotationPart;
    [SerializeField] private int _speedMin = 5;
    [SerializeField] private int _speedMax = 25;
    private float _speed;
    private float _attenuation = 0.95f;
    private Random _random = new Random();
    private bool _start;
    private bool _isThereSpin = true;
    private bool _canSpin;

    public void RotateWheel()
    {
        if (_isThereSpin && _canSpin)
        {
            _speed = _random.Next(_speedMin, _speedMax);
            _rotationPart.AddTorque(_speed, ForceMode2D.Impulse);
            _start = true;
            CountSpin.Invoke();
        }
    }

    public void SetAvailableForSpin(bool state)
    {
        _isThereSpin = state;
        _button.interactable = false;
    }
    private void Awake()
    {
        _rotationPart.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    private void Update()
    {
        RotationStop();
        CanSpin();
    }

    private void CanSpin()
    {
        _canSpin = _rotationPart.angularVelocity == 0;
    }
    private void FixedUpdate()
    {
        _rotationPart.angularVelocity *= _attenuation;
    }

    private void RotationStop()
    {
        if (Mathf.Abs(_rotationPart.angularVelocity) < 2f)
        {
            _rotationPart.angularVelocity = 0;
        }
        
        if (_rotationPart.angularVelocity == 0 && _start)
        {
            RotationStopEvent?.Invoke(true);
            Test.Invoke(_canSpin);
        }
        else
        {
            RotationStopEvent?.Invoke(false);
            Test.Invoke(_canSpin);
        }
    }
}
