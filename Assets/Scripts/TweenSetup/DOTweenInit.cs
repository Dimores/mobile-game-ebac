using UnityEngine;
using DG.Tweening;

public class DOTweenInit : MonoBehaviour
{
    void Awake()
    {
        DOTween.SetTweensCapacity(500, 50);
    }
}