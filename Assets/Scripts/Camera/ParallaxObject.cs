using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorSwizzling;

public class ParallaxObject : MonoBehaviour
{
    [SerializeField] private new CameraController camera;
    [SerializeField] private float parallaxFactor = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (1 - parallaxFactor) * camera.transform.position.xy0();
    }
}
