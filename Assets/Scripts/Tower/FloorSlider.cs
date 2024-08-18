using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VectorSwizzling;

public class FloorSlider : MonoBehaviour
{
    private Wobbleinator wobbleinator;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (height > wobbleinator.GetPivotHeight())
        {
            float h =  
                (height - wobbleinator.GetPivotHeight()) / 
                (wobbleinator.GetTopHeight() - wobbleinator.GetPivotHeight());


            float d = wobbleinator.GetFloorSlideDist() * wobbleinator.GetSinTilt();

            float x = -d * h * h;
            float y = x * wobbleinator.GetTanTilt();

            transform.localPosition = height._0x0() + x.x00();// + y._0x0();
        }
        else
            transform.localPosition = height._0x0();
    }

    public void Setup(Wobbleinator wb, float h)
    {
        wobbleinator = wb;

        SetHeight(h);
    }

    public void SetHeight(float h)
    {
        height = h;
    }
}
