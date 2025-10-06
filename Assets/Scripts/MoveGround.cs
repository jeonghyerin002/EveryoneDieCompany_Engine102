using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f);

        if(transform.position.x <= -20.0f)
        {
            transform.position += new Vector3(40.0f, 0.0f, 0.0f);
        }
    }
}
