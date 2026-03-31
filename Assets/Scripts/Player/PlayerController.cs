using DG.Tweening;
using Ebac.Core.Singleton;
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

    [Header("Animation")]
    public AnimatorManager animatorManager;
    [SerializeField] private BounceHelper _bounceHelper;

    [Header("VFX")]
    public ParticleSystem heightVfx;
    public int rateOverDistance = 6;
    #endregion

    #region PRIVATES
    private bool _canRun;
    private bool _isDead; // Nova flag para controlar o estado de morte
    private Vector3 _pos;
    private Vector3 _startPosition;
    private float _currentSpeed;
    private bool _invencible = false;
    private float _baseSpeedToAnimation = 7;
    #endregion

    void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();

        ChangeEmission(0);
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
        _isDead = false; // Garante que o player não está morto ao começar
        animatorManager.Play(AnimatorManager.AnimatonType.RUN, _currentSpeed / _baseSpeedToAnimation);
    }

    private void EndGame(AnimatorManager.AnimatonType animatonType = AnimatorManager.AnimatonType.IDLE)
    {
        _canRun = false;
        _isDead = true; // Avisa o sistema que o player morreu
        endScreen.SetActive(true);
        animatorManager.Play(animatonType);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isDead) return; // Se já estiver morto, ignora novas colisões

        if (collision.transform.tag == enemyTag)
        {
            if (!_invencible)
            {
                MoveBack(collision.transform);
                EndGame(AnimatorManager.AnimatonType.DEAD);
            }
        }
    }

    private void MoveBack(Transform t)
    {
        t.DOMoveZ(1f, .3f).SetRelative();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDead) return; // Se já estiver morto, ignora novos triggers

        if (other.transform.tag == endLineTag)
        {
            if (!_invencible) EndGame();
        }
    }

    #region ANIMATION
    public void Bounce()
    {
        if (_bounceHelper != null)
            _bounceHelper.Bounce();
    }
    #endregion

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

        ChangeEmission(rateOverDistance);

        transform.DOMoveY(_startPosition.y + amount,
            animationDuration).SetEase(ease);

        animatorManager.Play(AnimatorManager.AnimatonType.FLY);

        Invoke(nameof(ResetHeight), duration);
    }

    private void ReturnToRun()
    {
        animatorManager.Play(AnimatorManager.AnimatonType.RUN, _currentSpeed / _baseSpeedToAnimation);
        ChangeEmission(0);
    }

    public void ResetHeight()
    {
        //var p = transform.position;
        //p.y = _startPosition.y;
        //transform.position = p;

        transform.DOMoveY(_startPosition.y, .1f).OnComplete(ReturnToRun);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion

    #region VFX
    private void ChangeEmission(int value = 0)
    {
        var emissionModule = heightVfx.emission;
        emissionModule.rateOverDistance = new ParticleSystem.MinMaxCurve(value);
    }
    #endregion
}