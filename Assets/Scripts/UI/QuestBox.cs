using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestBox : MonoBehaviour
{
    [SerializeField] GameObject questPanel;
    [SerializeField] TextMeshProUGUI rewardLabel;
    [SerializeField] String[] rewardStrings;

    // Start is called before the first frame update
    void Start()
    {
        questPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool active)
    {
        questPanel.SetActive(active);
    }

    public void SetRewardLabel(int id)
    {
        rewardLabel.text = rewardStrings[id];
    }
}
