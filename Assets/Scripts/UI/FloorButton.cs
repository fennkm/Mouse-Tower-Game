using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloorButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Transform costPanel;
    [SerializeField] private int floorID;
    private TextMeshProUGUI[] costLabels;
    private Image[] costBoxes;
    private bool isActive;

    void Awake()
    {
        costLabels = costPanel.GetComponentsInChildren<TextMeshProUGUI>();
        costBoxes = costPanel.GetComponentsInChildren<Image>();

        SetButtonActive(false);
        for (int i = 0; i < 3; i++)
            SetCostLabelTint(i, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isActive) uiManager.ClickFloor(floorID);
    }

    public void SetCostLabels(int toothpicks, int strings, int buttons)
    {
        costLabels[0].text = toothpicks.ToString();
        costLabels[1].text = strings.ToString();
        costLabels[2].text = buttons.ToString();
    }

    public void SetAffordances(bool[] affordances)
    {
        SetButtonActive(affordances[0] && affordances[1] && affordances[2]);

        for (int i = 0; i < affordances.Length; i++)
            SetCostLabelTint(i, affordances[i]);
    }

    private void SetButtonActive(bool active)
    {
        isActive = active;

        Color col = GetComponent<Image>().color;
        col.a = isActive ? 1f : .5f;
        GetComponent<Image>().color = col;
    }

    private void SetCostLabelTint(int currencyType, bool active)
    {
        Image box = costBoxes[currencyType];

        box.color = active ? new Color(.2f, .6f ,.2f) : new Color(.3f, .3f ,.3f);
    }
}
