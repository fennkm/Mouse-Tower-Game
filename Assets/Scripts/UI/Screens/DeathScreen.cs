using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject[] deathLabels;
    [SerializeField] private GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in deathLabels)
            obj.SetActive(false);

        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearDeathScreen()
    {
        foreach (GameObject obj in deathLabels)
            obj.SetActive(false);

        menu.SetActive(false);
    }

    public void SetDeathScreen(int type)
    {
        deathLabels[type].SetActive(true);

        menu.SetActive(true);
    }
}
