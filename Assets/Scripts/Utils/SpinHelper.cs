using UnityEngine;
using DG.Tweening;

public class SpinHelper : MonoBehaviour
{
    [Header("Giro (Eixo Z - Como um disco)")]
    [SerializeField] private float _spinDuration = 2f;

    [Header("Flutuar (Eixo Y - Cima/Baixo)")]
    [SerializeField] private float _floatDistance = 0.5f;
    [SerializeField] private float _floatDuration = 1.5f;
    [SerializeField] private Ease _floatEase = Ease.InOutSine;

    void Start()
    {
        // Limpa qualquer tween antigo pra não dar conflito
        transform.DOKill();

        // 2. MOVIMENTO NO Y (Sobe e desce)
        // SetRelative(true) garante que ele use a posição atual como "zero"
        // e não saia voando para coordenadas globais.
        transform.DOLocalMoveZ(_floatDistance, _floatDuration)
            .SetRelative(true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(_floatEase)
            .SetLink(gameObject);
    }
}