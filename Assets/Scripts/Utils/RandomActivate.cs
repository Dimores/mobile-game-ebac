using UnityEngine;

public class RandomActivate : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(Random.value > 0.5f);
    }
}
