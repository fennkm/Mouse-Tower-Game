using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TowerManager towerManager;
    [SerializeField] private FloorButton[] floorButtons;
    [SerializeField] private UIMeter[] meterSliders;
    [SerializeField] private CurrencyLabel[] currencyLabels;
    [SerializeField] private PowerupLabel[] powerupLabels;
    [SerializeField] private QuestBox questBox;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private StartMenu startMenu;
    [SerializeField] private SettingsMenu settingsMenu;
    [SerializeField] private CreditsScreen creditsScreen;
    [SerializeField] private DeathScreen deathScreen;
    [SerializeField] private TextMeshProUGUI altitudeLabel;
    [SerializeField] private GameObject uiBlocker;
    private CameraController cameraController;

    // Start is called before the first frame update
    void Awake()
    {
        cameraController = camera.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMeterValues(int type, float value, float rate)
    {
        meterSliders[type].SetValues(value, rate);
    }

    public void SetCurrencyValues(int type, int value, int cap)
    {
        currencyLabels[type].SetValue(value, cap);
    }

    public void SetPowerupValues(int type, int value)
    {
        powerupLabels[type].SetValue(value);
    }

    public void UpdateFloorCostLabels(int type, int[] costs)
    {
        floorButtons[type].SetCostLabels(costs);
    }

    public void SetFloorAffordances(int type, bool[] affordance)
    {
        floorButtons[type].SetAffordances(affordance);
    }

    public void SetFloorHighlight(int type, bool active)
    {
        floorButtons[type].SetHightlight(active);
    }

    public void ShowQuest(int type)
    {
        questBox.ShowReward(type);
    }

    public void HideQuest()
    {
        questBox.HideReward();
    }

    public void SetQuestProgress(float fill)
    {
        questBox.SetProgressBar(fill);
    }

    public void SetAltitudeLabel(int floor)
    {
        altitudeLabel.text = (floor * 10).ToString();
    }

    public void ClickFloor(int floorID)
    {
        GameObject obj = Instantiate(towerManager.GetFloorObj(floorID));

        DraggableFloor draggable = obj.AddComponent<DraggableFloor>();
        draggable.Setup(towerManager, camera, floorID);

        audioManager.PlaySFX("FloorPickup");
    }

    public void ShowDeathScreen(int deathType, int floor)
    {
        deathScreen.SetDeathScreen(deathType, floor * 10);
    }

    public void ClearDeathScreen()
    {
        deathScreen.ClearDeathScreen();
    }

    public void SetStartScreen(bool active)
    {
        ClearDeathScreen();
        gameUI.SetActive(!active);
        startMenu.SetActive(active);
        settingsMenu.SetActive(active);
        creditsScreen.SetActive(active);

        ShowStart();
    }

    public void ShowSettings()
    {
        cameraController.MoveToHeight(settingsMenu.transform.position.y);
    }

    public void ShowCredits()
    {
        cameraController.MoveToHeight(creditsScreen.transform.position.y);
    }

    public void ShowStart()
    {
        cameraController.MoveToHeight(startMenu.transform.position.y);
    }

    public void SetUIBlocker(bool active)
    {
        uiBlocker.SetActive(active);
    }
}
