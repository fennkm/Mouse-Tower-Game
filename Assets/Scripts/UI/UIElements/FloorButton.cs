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
    [SerializeField] private Sprite questSprite;
    [SerializeField] private Sprite greyLabelSprite;
    [SerializeField] private GameObject disableOverlay;
    [SerializeField] private int floorID;
    private TextMeshProUGUI[] costLabels;
    private Sprite[] costLabelSprites = new Sprite[3];
    private Image[] costBoxes;
    private Sprite normalSprite;
    private bool isActive;

    void Awake()
    {
        costLabels = costPanel.GetComponentsInChildren<TextMeshProUGUI>();
        costBoxes = costPanel.GetComponentsInChildren<Image>();

        for (int i = 0; i < 3; i++)
            costLabelSprites[i] = costBoxes[i].sprite;

        normalSprite = GetComponent<Image>().sprite;

        SetButtonActive(false);

        for (int i = 0; i < 3; i++)
            SetCostLabelTint(i, false);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isActive) uiManager.ClickFloor(floorID);
    }

    public void SetCostLabels(int[] costs)
    {
        for (int i = 0; i < 3; i++)
            costLabels[i].text = costs[i].ToString();
    }

    public void SetAffordances(bool[] affordances)
    {
        SetButtonActive(affordances[0] && affordances[1] && affordances[2]);

        for (int i = 0; i < 3; i++)
            SetCostLabelTint(i, affordances[i]);
    }

    public void SetHightlight(bool active)
    {
        GetComponent<Image>().sprite = active ? questSprite : normalSprite;
    }

    private void SetButtonActive(bool active)
    {
        isActive = active;

        disableOverlay.SetActive(!active);
    }

    private void SetCostLabelTint(int currencyType, bool active)
    {
        costBoxes[currencyType].sprite = active ? costLabelSprites[currencyType] : greyLabelSprite;
    }
}
