using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterManager : MonoBehaviour
{
    public const int INSTABILITY = 0;
    public const int STRESS = 1;
    public const int DANGER = 2;

    [SerializeField] UIManager uiManager;
    private float[] meterRates = { 0f, 0f, 0f };
    private float[] meterVals = { 0f, 0f, 0f };
    private bool active;

    // Start is called before the first frame update
    void Awake()
    {
        SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        for (int i = 0; i < 3; i++)
        {
            meterVals[i] = Mathf.Clamp01(meterVals[i] + meterRates[i] * Time.deltaTime);

            uiManager.SetMeterValues(i, meterVals[i], meterRates[i]);
        }
        
    }

    public void SetMeterRate(int type, float rate)
    {
        meterRates[type] = Mathf.Clamp01(rate);
    }

    public void SetMeterVal(int type, float val)
    {
        meterVals[type] = Mathf.Clamp01(val);
    }

    public void ChangeMeterRate(int type, float rate)
    {
        meterRates[type] = Mathf.Clamp01(meterRates[type] + rate);
    }

    public void ChangeMeterVal(int type, float val)
    {
        meterVals[type] = Mathf.Clamp01(meterVals[type] + val);
    }

    public float GetMeterVal(int type)
    {
        return meterVals[type];
    }

    public float GetMeterRate(int type)
    {
        return meterRates[type];
    }

    public void SetActive(bool isActive)
    {
        active = isActive;
    }

    public void Reset()
    {
        meterRates = new float[] { 0f, 0f, 0f };
        meterVals = new float[] { 0f, 0f, 0f };
    }
}
