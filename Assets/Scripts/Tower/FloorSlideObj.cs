using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorSwizzling;

public class FloorSlideObj : MonoBehaviour
{
    [SerializeField] float leftMax;
    [SerializeField] float rightMax;
    private FloorSlider floorSlider;
    private float restPos;
    // Start is called before the first frame update
    void Start()
    {
        floorSlider = GetComponentInParent<FloorSlider>();

        restPos = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        float newPos;

        if (floorSlider.GetSinTilt() < 0)
        {
            newPos = Mathf.Lerp(
                restPos, 
                restPos + rightMax, 
                -floorSlider.GetSinTilt());
        }
        else
        {
            newPos = Mathf.Lerp(
                restPos, 
                restPos - leftMax, 
                floorSlider.GetSinTilt());
        }

        transform.localPosition = 
            transform.localPosition._0yz() + newPos.x00();
    }
}
