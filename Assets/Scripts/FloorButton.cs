using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloorButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private int floorID;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        uiManager.ClickFloor(floorID);
    }
}
