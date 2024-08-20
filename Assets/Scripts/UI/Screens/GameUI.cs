using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup ui;
    // Start is called before the first frame update
    void Awake()
    {
        SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool isActive)
    {
        ui.alpha = isActive ? 1 : 0;
        ui.blocksRaycasts = isActive;
    }
}
