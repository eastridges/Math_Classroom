using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class RHController : MonoBehaviour
{
    public InputReader input;
    public GameObject controller;
    public GameObject debugger;

    // Start is called before the first frame update
    void Start()
    {
        debugger.SetActive(true);
        controller.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (input.RightControllerFound)
        {
            debugger.SetActive(false);
            controller.SetActive(true);
        }
    }

    public void Setup(InputReader importInput,GameObject importController, GameObject importDebugger)
    {
        input=importInput;
        controller=importController;
        debugger=importDebugger;
    }
}
