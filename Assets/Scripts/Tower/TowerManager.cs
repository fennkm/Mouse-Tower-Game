using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using VectorSwizzling;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private new CameraController camera;
    [SerializeField] private MeterManager meterManager;
    [SerializeField] private GameObject floorObj;
    [SerializeField] private GameObject floorMarker;
    [SerializeField] private Color[] floorCols;
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

    public void PlaceFloor(Vector3 mousePos, int id)
    {
        if (!CheckFloorMarkerHitbox(mousePos)) return;

        currentHeight += floorHeight;

        GameObject newFloor = 
            Instantiate(
                floorObj, 
                transform.TransformPoint(currentHeight._0x0()), 
                transform.localRotation, 
                transform);

        newFloor.GetComponent<SpriteRenderer>().color = floorCols[id];

        camera.MoveToHeight(currentHeight);

        floorMarker.transform.localPosition = (currentHeight + floorHeight)._0x0();

        if (id == 2)
        {
            meterManager.SetMeterVal(0, 0);
            meterManager.SetMeterRate(0, 0);
        }
        else
            meterManager.ChangeMeterRate(0, 0.01f);
    }

    public Color GetFloorCol(int id)
    {
        return floorCols[id];
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
