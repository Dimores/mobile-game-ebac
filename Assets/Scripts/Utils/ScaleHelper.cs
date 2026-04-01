using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHelper : MonoBehaviour
{
    [Header("Animation")]
    public float scaleDuration = .2f;
    public float finalScale = 1.2f;
    public Ease ease = Ease.OutBack;

    public void Scale()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(finalScale, scaleDuration).SetEase(ease);
    }
}
