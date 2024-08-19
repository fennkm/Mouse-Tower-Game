using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreen : MonoBehaviour
{
    [SerializeField] private GameObject screen;
    
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
        screen.SetActive(isActive);
    }
}
