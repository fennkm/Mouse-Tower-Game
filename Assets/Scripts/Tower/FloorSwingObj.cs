using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwingObj : MonoBehaviour
{
    [SerializeField] float maxDegree;
    [SerializeField] bool invertSwing;
    private FloorSlider floorSlider;
    // Start is called before the first frame update
    void Start()
    {
        floorSlider = GetComponentInParent<FloorSlider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = 
            Quaternion.Euler(0, 0, maxDegree * floorSlider.GetSinTilt() * (invertSwing ? -1 : 1));
    }
}
