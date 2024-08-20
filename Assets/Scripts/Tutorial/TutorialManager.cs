using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    [SerializeField] GameObject[] tutorialImages;
    [SerializeField] GameObject prevArrow;
    [SerializeField] GameObject nextArrow;
    [SerializeField] GameObject backButton;
    private bool tutorialActive;
    private int tutorialFrame;

    // Start is called before the first frame update
    void Awake()
    {
        tutorialActive = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextTutorial()
    {
        if (!tutorialActive || tutorialFrame == tutorialImages.Length) return;

        ShowTutorial(tutorialFrame + 1);
    }

    public void PrevTutorial()
    {
        if (!tutorialActive || tutorialFrame == 0) return;

        ShowTutorial(tutorialFrame - 1);
    }

    public void StartTutorial()
    {
        tutorialActive = true;

        uiManager.SetStartScreen(false);

        prevArrow.SetActive(false);
        nextArrow.SetActive(true);
        backButton.SetActive(true);

        tutorialImages[0].SetActive(true);

        tutorialFrame = 0;
    }

    public void EndTutorial()
    {
        tutorialImages[tutorialFrame].SetActive(false);
        
        tutorialActive = false;

        uiManager.SetStartScreen(true);

        prevArrow.SetActive(false);
        nextArrow.SetActive(false);
        backButton.SetActive(false);
    }

    private void ShowTutorial(int frameNum)
    {
        if (frameNum == tutorialImages.Length)
        {
            EndTutorial();
            return;
        }

        tutorialImages[tutorialFrame].SetActive(false);

        if (frameNum == 0)
            prevArrow.SetActive(false);
        else
            prevArrow.SetActive(true);
        
        tutorialImages[frameNum].SetActive(true);

        tutorialFrame = frameNum;
    }
}
