using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestBox : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Image rewardImg;
    [SerializeField] private Image progressBar;
    [SerializeField] private Sprite[] rewardSprites;
    [SerializeField] private Color progressFullColor;
    [SerializeField] private Color progressMidColor;
    [SerializeField] private Color progressEmptyColor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool active)
    {
    }

    public void SetRewardLabel(int id)
    {
        rewardImg.sprite = rewardSprites[id];
    }

    public void SetProgressBar(float fill)
    {
        progressBar.fillAmount = fill;

        if (fill < .5f)
            progressBar.color = Color.Lerp(progressEmptyColor, progressMidColor, fill * 2);
        else
            progressBar.color = Color.Lerp(progressMidColor, progressFullColor, (fill - .5f) * 2);
    }

    public void ShowReward(int id)
    {
        SetRewardLabel(id);
        
        anim.SetTrigger("Popup");
    }

    public void HideReward()
    {
        anim.SetTrigger("Popdown");
    }
}
