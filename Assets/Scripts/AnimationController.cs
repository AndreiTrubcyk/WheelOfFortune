using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private GameObject _particalBackLight;
    [SerializeField] private Animator[] _showAnimators;
    [SerializeField] private Animator _chest;
    [SerializeField] private Animator _heart;
    [SerializeField] private Animator _diamond;
    [SerializeField] private Animator _coins;
    [SerializeField] private Animator scull;
    [SerializeField] private Animator _questionMark;
    [SerializeField] private GameObject _back;
    [SerializeField] private Button _clameButton;
    private Dictionary<string, Animator> _animation = new();
    private string _addToChestAnimation = "AddToChest";
    private string _chestShake = "ChestShake";
    private string _scullAnimation = "ScullAnimation";
    private string _openChest = "OpenChest";
    private string _openingAnimation = "OpeningAnimation";
    private string _claimAnimation = "ClaimAnimation";
    private Animator _currentAnimator;
    private bool _canOpenChest;

    private void Awake()
    {
        _animation.Add(Constants.HEART, _heart );
        _animation.Add(Constants.DIAMOND, _diamond );
        _animation.Add(Constants.COINS, _coins );
        _animation.Add(Constants.SCULL, scull );
        _animation.Add(Constants.QUESTION_MARK, _questionMark );
    }

    public void PlayParticle(bool state)
    {
        if (state)
        {
            _particalBackLight.SetActive(false);
        }
        else
        {
            _particalBackLight.SetActive(true);
        }
    }
    public void Claim()
    {
        foreach (var animator in _showAnimators)
        {
            animator.Play(_claimAnimation);
        }
        _clameButton.gameObject.SetActive(false);
        _back.SetActive(false);
    }
    public void PlayAnimation(string tag)
    {
        _back.SetActive(true);
        _currentAnimator = _animation[tag];
        StartCoroutine(Play(tag));
    }

    public void OpenChestAnimationAccessible()
    {
        _canOpenChest = true;
    }

    public void ShowPrizes()
    {
        _back.SetActive(true);
        _chest.Play("Idle");
        _clameButton.gameObject.SetActive(true);
        StartCoroutine(TempCor(_openingAnimation));
    }

    private IEnumerator TempCor(string animationName)
    {
        foreach (var animator in _showAnimators)
        {
            animator.Play(animationName);
            yield return new WaitForSeconds(1f);
        }

        _clameButton.interactable = true;
    }

    private IEnumerator Play(string tag)
    {
        if (tag != Constants.SCULL)
        {
            _currentAnimator.Play(_addToChestAnimation);
            yield return StartCoroutine(WaitForAnimationToEnd(_addToChestAnimation, _currentAnimator));
            _back.SetActive(false);
            _chest.Play(_chestShake);
            yield return StartCoroutine(WaitForAnimationToEnd(_chestShake, _chest));
        }
        else
        {
            _currentAnimator.Play(_scullAnimation);
            yield return StartCoroutine(WaitForAnimationToEnd(_scullAnimation, _currentAnimator));
            _back.SetActive(false);
        }

        if (_canOpenChest)
        {
            _chest.Play(_openChest);
        }
    }
    
    private IEnumerator WaitForAnimationToEnd(string animationName, Animator animator)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            yield return null;
        }

        while (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && 
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
    }
}
