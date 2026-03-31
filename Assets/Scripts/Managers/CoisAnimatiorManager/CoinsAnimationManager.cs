using DG.Tweening;
using Ebac.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBeetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    private List<ItemCollectableCoin> items;

    void Start()
    {
        items = new List<ItemCollectableCoin>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartAnimations();
        }
    }

    public void RegisterCoin(ItemCollectableCoin item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            item.transform.localScale = Vector3.zero;
        }
    }

    public void CleanCoins()
    {
        items.Clear();
    }

    public void StartAnimations()
    {
        StartCoroutine(ScalePiecesByTime());
    }

    IEnumerator ScalePiecesByTime()
    {
        foreach (var piece in items)
        {
            piece.transform.localScale = Vector3.zero;
        }

        Sort();

        yield return new WaitForEndOfFrame();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null) continue;

            items[i].transform.DOScale(1, scaleDuration).SetEase(ease)
                .SetLink(items[i].gameObject);

            yield return new WaitForSeconds(scaleTimeBeetweenPieces);
        }
    }

    public void Sort()
    {
        items = items.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}
