using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [Header("Controls")]
    public float velocity = 1f;

    private Vector2 pastPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // mousePosition Agora - mousePosition Antes
            Move(Input.mousePosition.x - pastPosition.x);
        }


        pastPosition = Input.mousePosition;
    }

    public void Move(float speed)
    {
        transform.position += Vector3.right * speed * velocity * Time.deltaTime;
    }
}
