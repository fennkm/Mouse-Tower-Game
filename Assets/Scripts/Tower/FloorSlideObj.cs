using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorSwizzling;

public class FloorSlideObj : MonoBehaviour
{
    [SerializeField] float leftMax;
    [SerializeField] float rightMax;
    [SerializeField] bool invertSlide;
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
        float newPos = Mathf.Lerp(
            restPos - leftMax, 
            restPos + rightMax, 
            (floorSlider.GetSinTilt() * (invertSlide ? -1 : 1) + 1) / 2);

        transform.localPosition = 
            transform.localPosition._0yz() + newPos.x00();
    }
}
