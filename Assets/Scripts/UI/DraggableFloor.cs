using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VectorSwizzling;

public class DraggableFloor : MonoBehaviour
{
    private TowerManager towerManager;
    private new Camera camera;
    private int floorID;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = MousePos();

        if (!Input.GetMouseButton(0)) 
        { 
            towerManager.PlaceFloor(MousePos(), floorID);

            Destroy(gameObject);
        } 
    }

    public void Setup(TowerManager tm, Camera cam, int id)
    {
        towerManager = tm;
        camera = cam;
        floorID = id;
        GetComponent<SpriteRenderer>().color = towerManager.GetFloorCol(floorID);

        towerManager.SetFloorMarker(true);

        transform.position = MousePos();
    }

    private Vector3 MousePos()
    {
        return camera.ScreenToWorldPoint(Input.mousePosition).xy0();
    }
}
