using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using VectorSwizzling;

public class TowerManager : MonoBehaviour
{
    public const int SCAFFOLDING = 0;
    public const int PENDULUM = 1;
    public const int WOODWORKER = 2;
    public const int TAILOR = 3;
    public const int JEWELLER = 4;
    public const int SPA = 5;
    public const int BARRACKS = 6;
    [SerializeField] private new CameraController camera;
    [SerializeField] private MeterManager meterManager;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private GameObject floorMarker;
    [SerializeField] private GameObject[] floorObjects;
    [SerializeField] private float floorHeight;
    private float currentHeight = -3.5f;
    private float currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        floorMarker.transform.position = (currentHeight + floorHeight)._0x0();

        SetFloorMarker(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlaceFloor(Vector3 mousePos, int floorID)
    {
        if (!CheckFloorMarkerHitbox(mousePos)) return;

        if (!currencyManager.PurchaseFloor(floorID)) return;

        currentHeight += floorHeight;

        GameObject newFloor = 
            Instantiate(
                floorObjects[floorID], 
                transform.TransformPoint(currentHeight._0x0()), 
                transform.localRotation, 
                transform);

        camera.MoveToHeight(currentHeight);

        floorMarker.transform.localPosition = (currentHeight + floorHeight)._0x0();

        switch (floorID)
        {
            case SCAFFOLDING:
                meterManager.ChangeMeterRate(MeterManager.INSTABILITY, 0.005f);
                break;
            case PENDULUM:
                meterManager.SetMeterVal(MeterManager.INSTABILITY, 0);
                meterManager.SetMeterRate(MeterManager.INSTABILITY, 0);
                break;
            case WOODWORKER:
                meterManager.ChangeMeterRate(MeterManager.INSTABILITY, 0.01f);
                meterManager.ChangeMeterRate(MeterManager.STRESS, 0.015f);
                meterManager.ChangeMeterRate(MeterManager.DANGER, 0.005f);
                currencyManager.ChangeCurrencyRate(CurrencyManager.TOOTHPICKS, 1f);
                break;
            case TAILOR:
                meterManager.ChangeMeterRate(MeterManager.INSTABILITY, 0.01f);
                meterManager.ChangeMeterRate(MeterManager.STRESS, 0.015f);
                meterManager.ChangeMeterRate(MeterManager.DANGER, 0.005f);
                currencyManager.ChangeCurrencyRate(CurrencyManager.STRING, 0.5f);
                break;
            case JEWELLER:
                meterManager.ChangeMeterRate(MeterManager.INSTABILITY, 0.01f);
                meterManager.ChangeMeterRate(MeterManager.STRESS, 0.015f);
                meterManager.ChangeMeterRate(MeterManager.DANGER, 0.005f);
                currencyManager.ChangeCurrencyRate(CurrencyManager.BUTTONS, 0.2f);
                break;
            case SPA:
                meterManager.ChangeMeterRate(MeterManager.INSTABILITY, 0.015f);
                meterManager.ChangeMeterRate(MeterManager.STRESS, -0.05f);
                meterManager.ChangeMeterVal(MeterManager.STRESS, -0.3f);
                meterManager.ChangeMeterRate(MeterManager.DANGER, 0.015f);
                break;
            case BARRACKS:
                meterManager.ChangeMeterRate(MeterManager.INSTABILITY, 0.015f);
                meterManager.ChangeMeterRate(MeterManager.STRESS, 0.015f);
                meterManager.ChangeMeterRate(MeterManager.DANGER, -0.05f);
                meterManager.ChangeMeterVal(MeterManager.DANGER, -0.3f);
                break;
            default:
                throw new System.ArgumentOutOfRangeException(floorID + " is an invalid meter ID!");
        }
    }

    public GameObject GetFloorObj(int id)
    {
        return floorObjects[id];
    }

    public void SetFloorMarker(bool active)
    {
        floorMarker.SetActive(active);
    }

    public float GetCurrentHeight()
    {
        return currentHeight;
    }

    public void SetAngle(float angle)
    {
        currentAngle = angle;
    }

    public float GetAngle()
    {
        return currentAngle;
    }

    bool CheckFloorMarkerHitbox(Vector3 pos)
    {
        Transform box = floorMarker.transform;

        return  pos.y > (box.position.y - box.localScale.y) &&
                pos.x < (box.position.x + box.localScale.x / 2) &&
                pos.x > (box.position.x - box.localScale.x / 2);
    }
}
