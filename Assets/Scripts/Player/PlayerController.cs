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
    public string endLineTag = "EndLine";

    public GameObject endScreen;
    #endregion

    #region PRIVATES
    private bool _canRun;
    private Vector3 _pos;
    #endregion

    void Start()
    {
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

    public void StartGame()
    {
        _canRun = true;
    }

    private void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == enemyTag)
        {
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == endLineTag)
        {
            EndGame();
        }
    }
}
