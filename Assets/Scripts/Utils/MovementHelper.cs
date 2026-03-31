using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : MonoBehaviour
{
    public List<Transform> positions;
    public float duration = 1f;

    private int _index;
    private int _lastIndex = -1;

    private float _decisionOffset;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _decisionOffset = Random.Range(0f, 0.5f);
    }

    private void Start()
    {
        _index = Random.Range(0, positions.Count);
        _lastIndex = _index;

        transform.localPosition = positions[_index].localPosition;

        StartCoroutine(MovementLoop());
    }

    private void ChooseNextIndex()
    {
        if (positions.Count <= 1)
            return;

        int newIndex;

        do
        {
            newIndex = Random.Range(0, positions.Count);
        }
        while (newIndex == _lastIndex);

        _lastIndex = newIndex;
        _index = newIndex;
    }

    private IEnumerator MovementLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_decisionOffset);

            _startPosition = transform.localPosition;
            _targetPosition = positions[_index].localPosition;

            float time = 0f;

            while (time < duration)
            {
                transform.localPosition = Vector3.Lerp(_startPosition, _targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = _targetPosition;

            ChooseNextIndex();
        }
    }
}
