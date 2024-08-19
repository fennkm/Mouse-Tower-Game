using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDecor : MonoBehaviour
{
    private Transform rope;
    private Vector3 anchorPos;

    // Start is called before the first frame update
    void Start()
    {
        rope = transform.GetChild(0);
        anchorPos = 2 * rope.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 disp = anchorPos - transform.position;
        float angle = Mathf.Atan(disp.x / disp.y) * 180 / Mathf.PI;

        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }
}
