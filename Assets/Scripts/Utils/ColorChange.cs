using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    public Color startColor = Color.white;

    private Color _correctColor;
    private float _duration = 2f;

    public MeshRenderer _meshRenderer;

    private void OnValidate()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _correctColor = _meshRenderer.materials[0].GetColor("_Color");

        LerpColor();
    }

    private void LerpColor()
    {
        _meshRenderer.materials[0].SetColor("_Color", startColor);
        _meshRenderer.materials[0].DOColor(_correctColor, "_Color", _duration).SetDelay(1f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LerpColor();
        }
    }
}
