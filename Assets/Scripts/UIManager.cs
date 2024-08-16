using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private TowerManager towerManager;
    [SerializeField] private Color[] floorCols;
    [SerializeField] private GameObject floorDraggable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickFloor(int floorID)
    {
        DraggableObj draggable = Instantiate(floorDraggable).GetComponent<DraggableObj>();
        draggable.Setup(towerManager, camera, floorCols[floorID]);
    }
}
