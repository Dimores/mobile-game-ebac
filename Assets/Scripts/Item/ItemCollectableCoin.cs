using Orby.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    [Header("Coin Specific Config")]
    public Collider coinCollider;
    public bool collect = false;
    public float lerp = 5f;
    public float minDistance = 1f;

    private void Start()
    {
        CoinsAnimationManager.Instance.RegisterCoin(this);
    }

    private void Update()
    {
        if (collect)
        {
            transform.position = Vector3.Lerp(transform.position,
                PlayerController.Instance.transform.position, lerp * Time.deltaTime);

            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
            {
                PlayerController.Instance.Bounce();

                // 1. CHAME O VFX AQUI! 
                // Agora a moeda já está na posição do player. O VFX vai se desvincular (null) 
                // na posição correta antes da moeda ser destruída.
                base.OnCollect();

                HideItens();
                Destroy(gameObject);
            }
        }
    }

    private void HideItens()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    protected override void OnCollect()
    {
        coinCollider.enabled = false;
        collect = true; // Inicia a viagem até o player

        // Dica: Se esse áudio for o barulho de "moeda entrando no bolso", 
        // talvez você queira movê-lo para o Update junto com o base.OnCollect()
        AudioManager.Instance.PlayAudioByTypeWithRandomPitch(AudioManager.AudioType.COINCOLLECT, new Vector2(0.95f, 1.05f), Random.Range(0.09f, 0.19f));
    }

    protected override void Collect()
    {
        OnCollect();
    }
}