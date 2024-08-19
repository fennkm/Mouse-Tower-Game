using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VectorSwizzling;

public class TowerManager : MonoBehaviour
{
    public const int SCAFFOLDING = 0;
    public const int PENDULUM = 1;
    public const int WOODWORKER = 2;
    public const int TAILOR = 3;
    public const int JEWELLER = 4;
    public const int SPA = 5;
    public const int BARRACKS = 6;
    [SerializeField] private new CameraController camera;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private MeterManager meterManager;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private PowerupManager powerupManager;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private GameObject floorMarker;
    [SerializeField] private Transform groundFloor;
    [SerializeField] private GameObject[] floorObjects;
    [SerializeField] private float floorHeight;
    [SerializeField] private float floorWidth;
    [SerializeField] private float cameraOffset;
    [SerializeField] private float markerOffset;
    private GameObject currentDraggable;
    private Queue<GameObject> recentFloors;
    private float currentHeight;
    private float currentAngle;
    private int  floorCount;
    private bool active;

    void Awake()
    {
        SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        recentFloors = new Queue<GameObject>();

        uiManager.SetStartScreen(true);

        audioManager.StartMenuMusic();

        floorCount = 0;

        currentHeight = groundFloor.position.y;

        floorMarker.transform.localPosition = groundFloor.position + (floorHeight + markerOffset)._0x0();

        SetFloorMarker(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        for (int i = 0; i < 3; i++)
            if (meterManager.GetMeterVal(i) >= 1)
            {
                LoseGame(i);
                break;
            }
    }

    public void PlaceFloor(Vector3 mousePos, int floorID)
    {
        SetFloorMarker(false);
        currentDraggable = null;

        if (!CheckFloorMarkerHitbox(mousePos)) return;

        if (!currencyManager.PurchaseFloor(floorID)) return;

        currentHeight += floorHeight;

        floorCount++;

        GameObject newFloor = 
            Instantiate(
                floorObjects[floorID], 
                transform.TransformPoint(currentHeight._0x0()), 
                transform.localRotation, 
                transform);

        newFloor.GetComponent<SpriteRenderer>().sortingOrder = -floorCount;

        newFloor.GetComponent<FloorSlider>().Setup(GetComponent<Wobbleinator>(), currentHeight);

        newFloor.GetComponent<FloorParticleController>().GoPoof();

        recentFloors.Enqueue(newFloor);

        if (recentFloors.Count > 3)
            recentFloors.Dequeue();

        if (floorCount % 100 == 0)
        {
            newFloor.GetComponent<FloorParticleController>().GoSparkle();
            audioManager.PlaySFX("MilestoneChime");
        }

        camera.MoveToHeight(currentHeight + cameraOffset);
        
        floorMarker.transform.position += floorHeight._0x0();

        uiManager.SetAltitudeLabel(floorCount);

        questManager.BuyFloor(floorID);

        camera.Screenshake();

        audioManager.PlaySFX("FloorPlace");

        switch (floorID)
        {
            case SCAFFOLDING:
                meterManager.ChangeMeterRate(MeterManager.INSTABILITY, 0.005f);
                break;
            case PENDULUM:
                meterManager.SetMeterVal(MeterManager.INSTABILITY, 0);
                meterManager.SetMeterRate(MeterManager.INSTABILITY, 0);
                audioManager.PlaySFX("PendulumBell");
                break;
            case WOODWORKER:
                meterManager.ChangeMeterRate(MeterManager.INSTABILITY, 0.01f);
                meterManager.ChangeMeterRate(MeterManager.STRESS, 0.015f);
                meterManager.ChangeMeterRate(MeterManager.DANGER, 0.005f);
                currencyManager.ChangeCurrencyRate(CurrencyManager.TOOTHPICKS, 1f);
                currencyManager.ChangeCurrencyCap(CurrencyManager.TOOTHPICKS, 5);
                audioManager.PlaySFX("WoodworkerSaw");
                break;
            case TAILOR:
                meterManager.ChangeMeterRate(MeterManager.INSTABILITY, 0.01f);
                meterManager.ChangeMeterRate(MeterManager.STRESS, 0.015f);
                meterManager.ChangeMeterRate(MeterManager.DANGER, 0.005f);
                currencyManager.ChangeCurrencyRate(CurrencyManager.STRING, 0.5f);
                currencyManager.ChangeCurrencyCap(CurrencyManager.STRING, 3);
                audioManager.PlaySFX("TailorScissors");
                break;
            case JEWELLER:
                meterManager.ChangeMeterRate(MeterManager.INSTABILITY, 0.01f);
                meterManager.ChangeMeterRate(MeterManager.STRESS, 0.015f);
                meterManager.ChangeMeterRate(MeterManager.DANGER, 0.005f);
                currencyManager.ChangeCurrencyRate(CurrencyManager.BUTTONS, 0.2f);
                currencyManager.ChangeCurrencyCap(CurrencyManager.BUTTONS, 1);
                audioManager.PlaySFX("JewellerHammer");
                break;
            case SPA:
                meterManager.ChangeMeterRate(MeterManager.INSTABILITY, 0.015f);
                meterManager.ChangeMeterRate(MeterManager.STRESS, -0.025f);
                meterManager.ChangeMeterVal(MeterManager.STRESS, -0.4f);
                meterManager.ChangeMeterRate(MeterManager.DANGER, 0.015f);
                audioManager.PlaySFX("SpaBubbles");
                break;
            case BARRACKS:
                meterManager.ChangeMeterRate(MeterManager.INSTABILITY, 0.015f);
                meterManager.ChangeMeterRate(MeterManager.STRESS, 0.015f);
                meterManager.ChangeMeterRate(MeterManager.DANGER, -0.025f);
                meterManager.ChangeMeterVal(MeterManager.DANGER, -0.4f);
                audioManager.PlaySFX("BarracksMarch");
                break;
            default:
                throw new System.ArgumentOutOfRangeException(floorID + " is an invalid floor ID!");
        }
    }

    public void TrackDraggable(GameObject draggable)
    {
        currentDraggable = draggable;
    }

    public GameObject GetFloorObj(int id)
    {
        return floorObjects[id];
    }

    public void SetFloorMarker(bool active)
    {
        floorMarker.SetActive(active);
    }

    public float GetCurrentHeight()
    {
        return currentHeight;
    }

    public void LoseGame(int lossType)
    {
        SetActive(false);
        meterManager.SetActive(false);
        currencyManager.SetActive(false);
        powerupManager.SetActive(false);
        questManager.SetActive(false);
        GetComponent<Wobbleinator>().SetActive(false);

        SetFloorMarker(false);

        if (currentDraggable != null)
            Destroy(currentDraggable);

        uiManager.ShowDeathScreen(lossType);

        audioManager.FadeMusicOut();
    }

    public void ResetGame()
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);

        currentHeight = groundFloor.position.y;
        floorMarker.transform.localPosition = groundFloor.position + (floorHeight + markerOffset)._0x0();

        floorCount = 0;

        recentFloors = new Queue<GameObject>();

        SetFloorMarker(false);

        meterManager.Reset();
        currencyManager.Reset();
        powerupManager.Reset();
        questManager.Reset();
        GetComponent<Wobbleinator>().Reset();

        camera.Reset();

        uiManager.SetAltitudeLabel(floorCount);
    }

    public void RestartGame()
    {
        ResetGame();

        StartGame();
    }

    public void StartGame()
    {
        uiManager.ClearScreens();

        audioManager.StartGameMusic();

        SetActive(true);
        meterManager.SetActive(true);
        currencyManager.SetActive(true);
        powerupManager.SetActive(true);
        questManager.SetActive(true);
        GetComponent<Wobbleinator>().SetActive(true);
    }

    public void DisplayMenu()
    {
        audioManager.StartMenuMusic();

        uiManager.ClearScreens();
        uiManager.SetStartScreen(true);

        ResetGame();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void SetActive(bool isActive)
    {
        active = isActive;
    }

    public void ApplyDecor(int type)
    {
        foreach (GameObject floor in recentFloors)
            floor.GetComponent<FloorDecorController>().SetDecor(type);
    }

    bool CheckFloorMarkerHitbox(Vector3 pos)
    {
        Transform box = floorMarker.transform;

        return  pos.y > (box.position.y - floorHeight) &&
                pos.x < (box.position.x + floorWidth / 2) &&
                pos.x > (box.position.x - floorWidth / 2);
    }
}
