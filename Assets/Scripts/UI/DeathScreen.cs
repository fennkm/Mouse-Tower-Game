using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject[] deathLabels;
    [SerializeField] GameObject resetButton;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in deathLabels)
            obj.SetActive(false);

        resetButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearDeathScreen()
    {
        foreach (GameObject obj in deathLabels)
            obj.SetActive(false);

        resetButton.SetActive(false);
    }

    public void SetDeathScreen(int type)
    {
        deathLabels[type].SetActive(true);

        resetButton.SetActive(true);
    }
}
