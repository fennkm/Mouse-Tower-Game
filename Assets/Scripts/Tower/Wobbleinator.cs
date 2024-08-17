using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobbleinator : MonoBehaviour
{
    [SerializeField] private MeterManager meterManager;
    [SerializeField] private float wobbleSpeed;
    [SerializeField] private float wobbleMagnitude;
    [SerializeField] private float pivotDepth;
    [SerializeField] private float jumpThreshold = 0.02f;
    [SerializeField] private float jumpSmoothing = 0.002f;
    private TowerManager towerManager;
    private float pivotHeight;
    private float wobbleAngle = 0f;
    private float instability = 0f;

    // Start is called before the first frame update
    void Start()
    {
        towerManager = GetComponent<TowerManager>();
        pivotHeight = towerManager.GetCurrentHeight() - pivotDepth;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAngle();

        towerManager.SetAngle(wobbleAngle);
    }

    void UpdateAngle()
    {
        pivotHeight = Mathf.Lerp(pivotHeight, towerManager.GetCurrentHeight() - pivotDepth, 0.01f);

        if (Mathf.Abs(instability - meterManager.GetMeterVal(0)) < jumpThreshold)
            instability = meterManager.GetMeterVal(0);
        else
            instability = Mathf.Lerp(instability, meterManager.GetMeterVal(0), jumpSmoothing);

        wobbleAngle = instability * wobbleMagnitude * Mathf.Sin(Time.unscaledTime * wobbleSpeed);

        float a = wobbleAngle * Mathf.PI / 180;
        float h = pivotHeight;
        transform.localPosition = new Vector3(h * Mathf.Sin(a), h * (1 - Mathf.Cos(a)), 0f);
        transform.localRotation = Quaternion.Euler(0f, 0f, wobbleAngle);
    }
}
