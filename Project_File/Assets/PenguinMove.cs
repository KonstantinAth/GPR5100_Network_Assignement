using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinMove : MonoBehaviour
{
    [SerializeField]
    float speed = 6f;
    [SerializeField]
    float center = 0;
    [SerializeField]
    float trueSpeed;

    [SerializeField]
    bool isHorizontal;
    // Start is called before the first frame update
    void Start()
    {
        if (isHorizontal)
            center = transform.position.x;
        else
            center = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHorizontal)
        transform.position = new Vector3(transform.position.x, transform.position.y, center + Mathf.PingPong(Time.time * trueSpeed, speed) - speed / 2f);
        else
            transform.position = new Vector3(center + Mathf.PingPong(Time.time * trueSpeed, speed) - speed / 2f, transform.position.y, transform.position.z);
    }
}

