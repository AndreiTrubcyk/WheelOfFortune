using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Chest _chest;
    [SerializeField] private DailySpin _dailySpin;
    [SerializeField] private Wheel _wheel;
    [SerializeField] private Point _point;
    [SerializeField] private AnimationController _animationController;

    private void Awake()
    {
        _wheel.RotationStopEvent += _point.SetStateOfRotate;
        _point.SentPresent += _animationController.PlayAnimation;
        _wheel.CountSpin += _dailySpin.CountSpin;
        _dailySpin.SpinRunOut += _wheel.SetAvailableForSpin;
        _point.SentPresent += _chest.AddPoints;
        _dailySpin.OpenChest += _animationController.OpenChestAnimationAccessible;
        _dailySpin.OpenChest += _chest.TurnOnButton;
        _chest.ShowPrizes += _animationController.ShowPrizes;
        _wheel.Test += _animationController.PlayParticle;
    }

    private void OnDestroy()
    {
        _wheel.RotationStopEvent -= _point.SetStateOfRotate;
        _point.SentPresent -= _animationController.PlayAnimation;
        _wheel.CountSpin -= _dailySpin.CountSpin;
        _dailySpin.SpinRunOut -= _wheel.SetAvailableForSpin;
        _point.SentPresent -= _chest.AddPoints;
        _dailySpin.OpenChest -= _animationController.OpenChestAnimationAccessible;
        _dailySpin.OpenChest += _chest.TurnOnButton;
        _chest.ShowPrizes += _animationController.ShowPrizes;
        _wheel.Test -= _animationController.PlayParticle;
    }
}
