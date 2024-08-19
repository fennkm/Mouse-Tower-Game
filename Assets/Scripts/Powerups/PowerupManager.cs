using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public const int TETHERS = 0;
    public const int FLOWERS = 1;
    public const int BARBS = 2;
    [SerializeField] UIManager uiManager;
    [SerializeField] AudioManager audioManager;
    [SerializeField] MeterManager meterManager;
    [SerializeField] int[] startingPowerups = new int[3];
    private int[] powerupVals = { 0, 0, 0 };
    private bool active;

    void Awake()
    {
        SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    private void UpdatePowerupLabels()
    {
        for (int i = 0; i < powerupVals.Length; i++)
            uiManager.SetPowerupValues(i, powerupVals[i]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;
    }

    public void UsePowerup(int powerupID)
    {
        switch (powerupID)
        {
            case TETHERS:
                meterManager.SetMeterVal(MeterManager.INSTABILITY, 0f);
                meterManager.SetMeterRate(MeterManager.INSTABILITY, 0f);
                powerupVals[TETHERS] = Mathf.Max(0, powerupVals[TETHERS] - 1);
                audioManager.PlaySFX("TethersCreak");
                break;
            case FLOWERS:
                meterManager.SetMeterVal(MeterManager.STRESS, 0f);
                meterManager.SetMeterRate(MeterManager.STRESS, 0f);
                powerupVals[FLOWERS] = Mathf.Max(0, powerupVals[FLOWERS] - 1);
                audioManager.PlaySFX("FlowersStrum");
                break;
            case BARBS:
                meterManager.SetMeterVal(MeterManager.DANGER, 0f);
                meterManager.SetMeterRate(MeterManager.DANGER, 0f);
                powerupVals[BARBS] = Mathf.Max(0, powerupVals[BARBS] - 1);
                audioManager.PlaySFX("BarbsShing");
                break;
            default:
                throw new System.ArgumentOutOfRangeException(powerupID + " is an invalid powerup ID!");
        }

        UpdatePowerupLabels();
    }

    public void AddPowerup(int powerupID, int count)
    {
        powerupVals[powerupID] += count;

        UpdatePowerupLabels();
    }
    public void SetActive(bool isActive)
    {
        active = isActive;
    }

    public void Reset()
    {
        for (int i = 0; i < startingPowerups.Length; i++)
            powerupVals[i] = startingPowerups[i];

        UpdatePowerupLabels();
    }
}
