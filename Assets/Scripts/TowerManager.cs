using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using VectorSwizzling;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject floorObj;
    [SerializeField] private new CameraController camera;
    [SerializeField] private GameObject floorMarker;
    private float currentHeight = -3.5f;

    // Start is called before the first frame update
    void Start()
    {
        floorMarker.transform.position = (currentHeight + 2f)._0x0();

        SetFloorMarker(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlaceFloor(Vector3 mousePos, Color col)
    {
        if (!CheckFloorMarkerHitbox(mousePos)) return;

        currentHeight += 2;

        GameObject newFloor = 
            Instantiate(
                floorObj, 
                currentHeight._0x0(), 
                Quaternion.identity, 
                transform);

        newFloor.GetComponent<SpriteRenderer>().color = col;

        camera.MoveToHeight(currentHeight);

        floorMarker.transform.position = (currentHeight + 2f)._0x0();
    }

    public void SetFloorMarker(bool active)
    {
        floorMarker.SetActive(active);
    }

    float GetCurrentHeight()
    {
        return currentHeight;
    }

    bool CheckFloorMarkerHitbox(Vector3 pos)
    {
        Transform box = floorMarker.transform;

        return  pos.y > (box.position.y - box.localScale.y / 2) &&
                pos.x < (box.position.x + box.localScale.x / 2) &&
                pos.x > (box.position.x - box.localScale.x / 2);
    }
}
