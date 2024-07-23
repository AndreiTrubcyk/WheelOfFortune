using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public Action ShowPrizes;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _diamondText;
    [SerializeField] private TextMeshProUGUI _heartText;
    [SerializeField] private TextMeshProUGUI _mysteryText;
    private int _coins;
    private int _diamonds;
    private int _heart;
    private int _mystery;

    private void Awake()
    {
        _button.interactable = false;
    }

    public void AddPoints(string tag)
    {
        switch (tag)
        {
            case Constants.HEART:
                _heart += Constants.HEART_POINT;
                _heartText.text = _heart.ToString();
                break;
            case Constants.COINS:
                _coins += Constants.COINS_POINT;
                _coinsText.text = _coins.ToString();
                break;
            case Constants.DIAMOND:
                _diamonds += Constants.DIAMOND_POINT;
                _diamondText.text = _diamonds.ToString();
                break;
            case Constants.QUESTION_MARK:
                _mystery += Constants.QUESTION_POINT;
                _mysteryText.text = _mystery.ToString();
                break;
        }
    }

    public void ShowChestContents()
    {
        ShowPrizes.Invoke();
    }

    public void TurnOnButton()
    {
        _button.interactable = true;
    }
}
