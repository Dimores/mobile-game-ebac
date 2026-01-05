using DG.Tweening;
using Orby.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    #region PUBLICS
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 0.1f;
    public float speed = 1f;

    [Header("Tags")]
    public string enemyTag = "Enemy";
    public string endLineTag = "EndLine";

    [Header("UI")]
    public GameObject endScreen;

    [Header("TextMeshPro")]
    public TextMeshPro uiTextPowerUp;

    [Header("Coin Setup")]
    public GameObject coinCollector;
    #endregion

    #region PRIVATES
    private bool _canRun;
    private Vector3 _pos;

    private Vector3 _startPosition;

    private float _currentSpeed;

    private bool _invencible = false;
    #endregion

    void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }

    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
    }

    public void StartGame()
    {
        _canRun = true;
    }

    private void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == enemyTag)
        {
            if (!_invencible) EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == endLineTag)
        {
            if (!_invencible) EndGame();
        }
    }

    #region POWER UPS
    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }
    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }
    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvencible(bool b = true)
    {
        _invencible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        //var p = transform.position;
        //p.y = _startPosition.y + amount;
        //transform.position = p;

        transform.DOMoveY(_startPosition.y + amount,
            animationDuration).SetEase(ease);//.OnComplete(ResetHeight);

        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight()
    {
        //var p = transform.position;
        //p.y = _startPosition.y;
        //transform.position = p;

        transform.DOMoveY(_startPosition.y, .1f);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion
}
