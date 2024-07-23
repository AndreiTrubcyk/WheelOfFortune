using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Point : MonoBehaviour
{
    public Action<string> SentPresent;
    private Rigidbody2D _rigidbody2D;
    private bool _isRotationStopped;
    private bool _hasLogged;

    public void SetStateOfRotate(bool state)
    {
        _isRotationStopped = state;
    }
    
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (_isRotationStopped && !_hasLogged)
        {
            var tag = collider.tag;
            switch (tag)
            {
                case Constants.HEART: SentPresent?.Invoke(tag);
                    break;
                case Constants.DIAMOND: SentPresent?.Invoke(tag);
                    break;
                case Constants.COINS: SentPresent?.Invoke(tag);
                    break;
                case Constants.SCULL: SentPresent?.Invoke(tag);
                    break;
                case Constants.QUESTION_MARK: SentPresent?.Invoke(tag);
                    break;
            }
            _hasLogged = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _hasLogged = false;
    }
}
