using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using Unity.Services.Vivox;

public class GameController : NetworkBehaviour
{
    //So you can get user inputs
    public InputReader inputs;
    public TestRelay relayStarter;
    public GameObject inputField;
    public Vivox VivoxManager;

    public Transform rh;
    public Transform lh;
    public MultiplayerControl multiControl;
    public pointerControl pointer;

    public GameObject Ball;

    private string joinCode="";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inputs.ButtonADown)
        {
            //relayStarter.CreateRelay();
            /*
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Client started");
            }
            else
            {
                Debug.Log("Client failed to Start");
            }
            */
        }
        if (inputs.ButtonBDown)
        {
            //string joinCode = inputField.GetComponent<TMP_InputField>().text;
            //relayStarter.JoinRelay(joinCode);
            /*
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Host Started");
            }
            else
            {
                Debug.Log("Host failed to Start");
            }
            */
        }
        if (inputs.ButtonYDown)
        {
            Debug.Log(inputField.GetComponent<TMP_InputField>().text);
        }
        //reload the scene if the user presses x
        if(inputs.ButtonXDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Make a ball
        if(inputs.LeftMainTriggerDown)
        {
            multiControl.SpawnBallServerRpc(lh.position.x, lh.position.y, lh.position.z);
        }

        //Get input from keyboard and update the joincode string
        if (inputs.RightMainTriggerDown)
        {
            if (pointer.currentLetter=="Clear")
            {
                joinCode = "";
                inputField.GetComponent<TMP_InputField>().text = joinCode;
            }
            else if (pointer.currentLetter == "Enter")
            {
                if (joinCode == "")
                {
                    relayStarter.CreateRelay();
                    VivoxManager.InitializeAsync();
                    //VivoxManager.LoginToVivoxAsync();
                    //VivoxManager.JoinEchoChannelAsync();
                }
                else
                {
                    relayStarter.JoinRelay(joinCode);
                }
            }
            else
            {
                joinCode = joinCode + pointer.currentLetter;
                inputField.GetComponent<TMP_InputField>().text = joinCode;
            }
            
        }

        

    }

    
}
