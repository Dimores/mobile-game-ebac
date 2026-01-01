using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region PUBLICS
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 0.1f;

    public float speed = 1f;

    public string enemyTag = "Enemy";
    #endregion

    #region PRIVATES
    private bool _canRun;
    private Vector3 _pos;
    #endregion

    void Start()
    {
        _canRun = true;
    }

    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.Translate(transform.forward * speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == enemyTag)
        {
            _canRun = false;
        }
    }
}
