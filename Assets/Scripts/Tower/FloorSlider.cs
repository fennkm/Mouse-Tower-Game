using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VectorSwizzling;

public class FloorSlider : MonoBehaviour
{
    private Wobbleinator wobbleinator;
    private float height;
    private bool active;

    // Start is called before the first frame update
    void Awake()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        if (height > wobbleinator.GetPivotHeight())
        {
            float h =  
                (height - wobbleinator.GetPivotHeight()) / 
                (wobbleinator.GetTopHeight() - wobbleinator.GetPivotHeight());


            float d = wobbleinator.GetFloorSlideDist() * wobbleinator.GetSinTilt();

            float x = -d * h * h;

            transform.localPosition = height._0x0() + x.x00();
        }
        else
            transform.localPosition = height._0x0();
    }

    public void Setup(Wobbleinator wb, float h)
    {
        wobbleinator = wb;

        SetHeight(h);

        active = true;
    }

    public void SetHeight(float h)
    {
        height = h;
    }

    public float GetSinTilt()
    {
        if (!active) return 0;

        return wobbleinator.GetSinTilt();
    }
}
