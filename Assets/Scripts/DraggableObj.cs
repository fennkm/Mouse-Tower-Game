using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VectorSwizzling;

public class DraggableObj : MonoBehaviour
{
    private TowerManager towerManager;
    private new Camera camera;

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
            towerManager.PlaceFloor(MousePos(), GetComponent<SpriteRenderer>().color);

            Destroy(gameObject);
        } 
    }

    public void Setup(TowerManager tm, Camera cam, Color col)
    {
        towerManager = tm;
        camera = cam;
        GetComponent<SpriteRenderer>().color = col;

        towerManager.SetFloorMarker(true);

        transform.position = MousePos();
    }

    private Vector3 MousePos()
    {
        return camera.ScreenToWorldPoint(Input.mousePosition).xy0();
    }
}
