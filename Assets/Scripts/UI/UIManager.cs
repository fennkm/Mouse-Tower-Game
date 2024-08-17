using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private TowerManager towerManager;
    [SerializeField] private UIMeter[] meterSliders;
    [SerializeField] private GameObject floorDraggable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetMeterValues(int type, float value, float rate)
    {
        meterSliders[type].SetValues(value, rate);
    }

    public void ClickFloor(int floorID)
    {
        DraggableFloor draggable = Instantiate(floorDraggable).GetComponent<DraggableFloor>();
        draggable.Setup(towerManager, camera, floorID);
    }
}
