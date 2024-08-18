using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using VectorSwizzling;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveRate = .01f;
    private Animator animator;
    private Coroutine moveAnimation;
    private bool cameraMoving;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToHeight(float newHeight)
    {
        if (newHeight > transform.position.y)
        {
            if (cameraMoving)
                StopCoroutine(moveAnimation);

            moveAnimation = StartCoroutine(SmoothToHeight(newHeight));
        }
    }

    public void Screenshake()
    {
        animator.SetTrigger("ScreenShake");
    }

    IEnumerator SmoothToHeight(float height)
    {
        cameraMoving = true;

        while (Mathf.Abs(transform.position.y - height) > 0.01)
        {
            SetHeight(Mathf.Lerp(transform.position.y, height, moveRate));
            yield return null;
        }
        
        SetHeight(height);

        cameraMoving = false;
    }

    private void SetHeight(float height)
    {
        transform.position = transform.position.x0z() + height._0x0();
    }

    public void Reset()
    {
        SetHeight(0);
    }
}
