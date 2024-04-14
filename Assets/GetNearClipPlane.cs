using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNearClipPlane : MonoBehaviour
{
    public Camera myCamera; // Replace with your camera's reference

    void Start()
    {
        float nearClipPlaneValue = myCamera.nearClipPlane;
        Debug.Log("Current near clipping plane value: " + nearClipPlaneValue);
        myCamera.nearClipPlane = .1f;
    }

    private void Update()
    {
        float nearClipPlaneValue = myCamera.nearClipPlane;
        Debug.Log("Current near clipping plane value: " + nearClipPlaneValue);
    }
}
