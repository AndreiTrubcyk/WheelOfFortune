using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DailySpin : MonoBehaviour
{
    public Action<bool> SpinRunOut;
    public Action OpenChest;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int _maxPoints = 3;

    private void Awake()
    {
        _text.text = $"x {_maxPoints.ToString()}";
    }
    
    public void CountSpin()
    {
        _maxPoints -= 1;
        
        if (_maxPoints > 0)
        {
            _text.text = $"x {_maxPoints.ToString()}";
        }
        else
        {
            _text.text = "x 0";
            SpinRunOut?.Invoke(false);
            OpenChest?.Invoke();
        }
    }
}
