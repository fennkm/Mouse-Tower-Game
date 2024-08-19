using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustParticles;
    [SerializeField] private ParticleSystem starParticles;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoPoof()
    {
        dustParticles.Play();
    }

    public void GoSparkle()
    {
        starParticles.Play();
    }
}
