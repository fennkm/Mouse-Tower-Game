using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PowerupManager powerupManager;
    [SerializeField] private float questScoreReq;
    [SerializeField] private float floorScoreMin;
    [SerializeField] private float floorScoreMax;
    [SerializeField] private int[] questFloors;
    [SerializeField] private int[] questRewards;
    [SerializeField] private float questTime;
    private bool questActive;
    private int questFloor;
    private int questReward;
    private float questScore;
    private Coroutine questTimer;
    private bool active;

    void Awake()
    {
        SetActive(false);
    }

    void Start()
    {
        questActive = false;
        questScore = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;
    }

    public void BuyFloor(int type)
    {
        if(!questActive)
        {
            questScore += Random.Range(floorScoreMin, floorScoreMax);

            if (questScore > questScoreReq)
            {   
                questScore -= questScoreReq;
                GiveQuest();
            }
        }
        else if (type == questFloor)
            CompleteQuest();
    }

    public void GiveQuest()
    {
        questFloor = ChooseRandom(questFloors);
        questReward = ChooseRandom(questRewards);
        questActive = true;

        uiManager.SetFloorHighlight(questFloor, true);
        uiManager.ShowQuest(questReward);

        questTimer = StartCoroutine("QuestTimer");
    }

    public void CompleteQuest()
    {
        powerupManager.AddPowerup(questReward, 1);

        EndQuest();
    }

    public void SetActive(bool isActive)
    {
        if (!isActive && questActive)
            EndQuest();

        active = isActive;
    }

    public void Reset()
    {
        if (questActive)
            EndQuest();
            
        questScore = 0f;
    }

    private void EndQuest()
    {
        uiManager.SetFloorHighlight(questFloor, false);
        uiManager.HideQuest();

        questActive = false;

        StopCoroutine(questTimer);
    }

    private T ChooseRandom<T>(T[] arr)
    {
        return arr[Mathf.FloorToInt(Random.value * arr.Length)];
    }

    private IEnumerator QuestTimer()
    {
        float timer = questTime;

        while (timer > 0)
        {
            yield return null;

            timer -= Time.deltaTime;

            uiManager.SetQuestProgress(timer / questTime);
        }

        EndQuest();
    }
}
