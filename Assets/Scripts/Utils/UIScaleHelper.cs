using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleHelper : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private RectTransform _rectTransform;
    public float scaleDuration = .2f;
    public float finalScale = 1.2f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        Scale();
    }

    public void Scale()
    {
        _rectTransform.localScale = Vector3.zero;
        _rectTransform.DOScale(finalScale, scaleDuration).SetEase(ease);
    }
}
