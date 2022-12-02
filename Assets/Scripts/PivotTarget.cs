using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotTarget : MonoBehaviour
{
    private float angle;
    private float curPos;
    private float prevPos;
    private float speed;

    void Start()
    {
        curPos = 0;
        prevPos = 0;
        angle = 0;
        speed = 0.1f;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
            prevPos = Input.mousePosition.x;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            curPos = Input.mousePosition.x;
            angle = curPos - prevPos;
            transform.eulerAngles += new Vector3(transform.eulerAngles.x, angle * speed, transform.eulerAngles.z);
            prevPos = curPos;
        }
    }
}
