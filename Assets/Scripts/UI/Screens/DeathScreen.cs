using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject[] deathScreens;
    [SerializeField] private TextMeshProUGUI[] altitudes;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in deathScreens)
            obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearDeathScreen()
    {
        foreach (GameObject obj in deathScreens)
            obj.SetActive(false);
    }

    public void SetDeathScreen(int type, int altitude)
    {
        deathScreens[type].SetActive(true);
        altitudes[type].text = altitude.ToString();
    }
}
