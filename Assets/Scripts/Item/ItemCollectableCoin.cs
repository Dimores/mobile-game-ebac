using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using DG.Tweening;
//using Orby.Managers; // Importa o DOTween

public class ItemCollectableCoin : ItemCollectableBase
{
    [Header("Coin Specific Config")]
    public Collider coinCollider;
    public bool collect = false;
    public float lerp = 5f;
    public float minDistance = 1f;

    //[Header("Animation Config")]
    //public float rotationSpeed = 1f; 
    //public float floatHeight = 0.2f; 
    //public float floatDuration = 1f;


    //[Header("VFX Collider")]
    //public Transform vfxCollider;


    //private Tween _rotationTween;
    //private Tween _floatTween;


    private void Start()

    {
        //AnimateCoin(); 
        //CoinsAnimationManager.Instance.RegisterCoin(this);
    }

    private void Update()
    {
        if (collect)
        {
            transform.position = Vector3.Lerp(transform.position,
                PlayerController.Instance.transform.position, lerp * Time.deltaTime);

            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
            {
                HideItens();
                Destroy(gameObject);
            }
        }
    }

    private void HideItens()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }


    private void AnimateCoin()
    {
        //_floatTween = transform.DOMoveY(transform.position.y + floatHeight, floatDuration)
        //    .SetLoops(-1, LoopType.Yoyo) 
        //    .SetEase(Ease.InOutSine); 
    }

    protected override void OnCollect()
    {
        base.OnCollect();
        coinCollider.enabled = false;
        collect = true;
        //PlayerController.Instance.Bounce();

        //ItemManager.Instance.AddCoins();
        //VFXManager.Instance.PlayVFXByTypeWithCollision(VFXManager.VFXType.COIN, this.transform.position, 
        //    null, vfxCollider);
    }

    protected override void Collect()
    {
        OnCollect();
    }
}