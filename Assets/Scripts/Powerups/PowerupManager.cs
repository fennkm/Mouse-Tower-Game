using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public const int TETHERS = 0;
    public const int FLOWERS = 1;
    public const int BARBS = 2;
    [SerializeField] MeterManager meterManager;
    [SerializeField] PowerupLabel[] powerupLabels;
    [SerializeField] int[] startingPowerups = new int[3];
    private int[] powerupVals = { 0, 0, 0 };
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startingPowerups.Length; i++)
            powerupVals[i] = startingPowerups[i];

        UpdatePowerupLabels();
    }

    private void UpdatePowerupLabels()
    {
        for (int i = 0; i < powerupLabels.Length; i++)
            powerupLabels[i].SetValue(powerupVals[i]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UsePowerup(int powerupID)
    {
        switch (powerupID)
        {
            case TETHERS:
                meterManager.SetMeterVal(MeterManager.INSTABILITY, 0f);
                meterManager.SetMeterRate(MeterManager.INSTABILITY, 0f);
                powerupVals[TETHERS] = Mathf.Max(0, powerupVals[TETHERS] - 1);
                break;
            case FLOWERS:
                meterManager.SetMeterVal(MeterManager.STRESS, 0f);
                meterManager.SetMeterRate(MeterManager.STRESS, 0f);
                powerupVals[FLOWERS] = Mathf.Max(0, powerupVals[FLOWERS] - 1);
                break;
            case BARBS:
                meterManager.SetMeterVal(MeterManager.DANGER, 0f);
                meterManager.SetMeterRate(MeterManager.DANGER, 0f);
                powerupVals[BARBS] = Mathf.Max(0, powerupVals[BARBS] - 1);
                break;
            default:
                throw new System.ArgumentOutOfRangeException(powerupID + " is an invalid powerup ID!");
        }

        UpdatePowerupLabels();
    }
}
