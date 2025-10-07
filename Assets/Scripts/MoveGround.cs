using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public float speed = 1f;

    void Start()
    {
        
    }


    void Update()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f);

        if(transform.position.x <= 20.0f)
        {
            transform.position += new Vector3(1.0f, 0.0f, 0.0f);
        }
    }
}
