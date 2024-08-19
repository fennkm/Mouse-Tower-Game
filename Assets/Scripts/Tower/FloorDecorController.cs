using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDecorController : MonoBehaviour
{
    [SerializeField] private GameObject tethersDecor;
    [SerializeField] private GameObject flowersDecor;
    [SerializeField] private GameObject barbsDecor;
    // Start is called before the first frame update
    void Start()
    {
        tethersDecor.SetActive(false);
        flowersDecor.SetActive(false);
        barbsDecor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDecor(int type)
    {
        switch (type)
        {
            case 0:
                tethersDecor.SetActive(true);
                break;
            case 1:
                flowersDecor.SetActive(true);
                break;
            case 2:
                barbsDecor.SetActive(true);
                break;
            default:
                throw new System.ArgumentOutOfRangeException(type + " is an invalid powerup ID!");
        }
    }
}
