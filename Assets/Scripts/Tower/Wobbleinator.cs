using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobbleinator : MonoBehaviour
{
    [SerializeField] private MeterManager meterManager;
    [SerializeField] private float wobbleSpeed;
    [SerializeField] private float wobbleMagnitude;
    [SerializeField] private float pivotDepth;
    [SerializeField] private float floorSlideDistance;
    [SerializeField] private float maxWobbleHeight = 10f;
    [SerializeField] private float minWobbleHeight = 4f;
    [SerializeField] private float jumpThreshold = 0.02f;
    [SerializeField] private float jumpSmoothing = 0.002f;
    private TowerManager towerManager;
    private float pivotHeight;
    private float currentHeight;
    private float wobbleAngle = 0f;
    private float instability = 0f;
    private bool active;

    void Awake()
    {
        active = false;
        towerManager = GetComponent<TowerManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        UpdateAngle();

        // towerManager.SetAngle(wobbleAngle);
    }

    void UpdateAngle()
    {
        currentHeight = Mathf.Lerp(currentHeight, towerManager.GetCurrentHeight(), 0.01f);
        pivotHeight = Mathf.Lerp(pivotHeight, towerManager.GetCurrentHeight() - pivotDepth, 0.01f);

        if (Mathf.Abs(instability - meterManager.GetMeterVal(0)) < jumpThreshold)
            instability = meterManager.GetMeterVal(0);
        else
            instability = Mathf.Lerp(instability, meterManager.GetMeterVal(0), jumpSmoothing);

        float heightFactor =  Mathf.Clamp01((currentHeight - minWobbleHeight) / (maxWobbleHeight - minWobbleHeight));
        wobbleAngle = instability * heightFactor * wobbleMagnitude * Mathf.Sin(Time.unscaledTime * wobbleSpeed);

        float a = wobbleAngle * Mathf.PI / 180;
        float h = pivotHeight;
        transform.localPosition = new Vector3(h * Mathf.Sin(a), h * (1 - Mathf.Cos(a)), 0f);
        transform.localRotation = Quaternion.Euler(0f, 0f, wobbleAngle);
    }
    
    // private float WobbleCurve()
    // {
    //     return        Mathf.Sin((       Time.unscaledTime       ) * wobbleSpeed) + 
    //             .5f * Mathf.Sin(( .8f * Time.unscaledTime +   1f) * wobbleSpeed) +
    //            -.2f * Mathf.Sin((1.6f * Time.unscaledTime + -.4f) * wobbleSpeed);
    // }

    public float GetFloorSlideDist()
    {
        return floorSlideDistance;
    }

    public float GetTopHeight()
    {
        return currentHeight;
    }

    public float GetPivotHeight()
    {
        return pivotHeight;
    }

    public float GetAngle()
    {
        return wobbleAngle;
    }

    public float GetSinTilt()
    {
        return wobbleAngle / wobbleMagnitude;
    }

    public float GetTanTilt()
    {
        return Mathf.Tan(wobbleAngle * Mathf.PI / 180);
    }

    public void SetActive(bool isActive)
    {
        active = isActive;
    }

    public void Reset()
    {
        currentHeight = towerManager.GetCurrentHeight();
        pivotHeight = towerManager.GetCurrentHeight() - pivotDepth;
    }
}
