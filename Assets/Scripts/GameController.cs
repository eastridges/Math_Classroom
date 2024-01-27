using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox;
using Unity.Collections;
using System.Threading.Tasks;

public class GameController : NetworkBehaviour
{
    //So you can get user inputs
    public InputReader inputs;
    public TestRelay relayStarter;
    public GameObject inputField;
    public TextMeshPro instructions;

    public Transform rh;
    public Transform lh;
    public MultiplayerControl multiControl;
    public pointerControl pointer;

    public GameObject Ball;

    public static string joinCode="";
    public static string nickName = "";
    private bool joinedRelay = false;
    private keyInfo pressedKey;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void startMic()
    {
        string micName = Microphone.devices[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (inputs.ButtonYDown)
        {
            
        }
        if (inputs.ButtonBDown)
        {
            
        }
        //reload the scene if the user presses x
        if (inputs.ButtonXDown)
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
            if (pointer.currentKey != null)
            {
                pressedKey = pointer.currentKey;
            }

            //typing the relay room name
            if (!joinedRelay)
            {
                if (pointer.currentLetter=="Clear")
                {
                    joinCode = "";
                    inputField.GetComponent<TMP_InputField>().text = joinCode;
                }
                else if (pointer.currentLetter == "Enter")
                {
                    joinedRelay=true;
                    inputField.GetComponent<TMP_InputField>().text = nickName;
                    instructions.SetText("Pick a Username:");
                }
                else if (pointer.currentLetter == "Back")
                {
                    if (joinCode.Length>0)
                    {
                        joinCode = joinCode.Remove(joinCode.Length - 1);
                        inputField.GetComponent<TMP_InputField>().text = joinCode;
                    }
                }
                else
                {
                    joinCode = joinCode + pointer.currentLetter;
                    inputField.GetComponent<TMP_InputField>().text = joinCode;
                }

                if (pointer.currentKey != null)
                {
                    pressedKey.MakeBigger();
                }
            }
            //typing in name to display and joining relay and vivox
            else
            {
                if (pointer.currentKey != null)
                {
                    pressedKey.MakeBigger();
                }


                if (pointer.currentLetter=="Clear")
                {
                    nickName = "";
                    inputField.GetComponent<TMP_InputField>().text = nickName;
                }
                else if (pointer.currentLetter == "Back")
                {
                    if (nickName.Length > 0)
                    {
                        nickName = nickName.Remove(nickName.Length - 1);
                        inputField.GetComponent<TMP_InputField>().text = nickName;
                    }
                }
                else if (pointer.currentLetter == "Enter")
                {
                    if ((joinCode == "") && (nickName != ""))
                    {
                        relayStarter.CreateRelay();
                        startVivoxVoice(nickName);
                        //stores the relayroomcode in the TestRelay script
                        SceneManager.LoadScene("SecondScene");
                    }
                    else if (nickName != "")
                    {
                        relayStarter.JoinRelay(joinCode);
                        startVivoxVoice(nickName);
                        Variables.joinCode = joinCode;
                        SceneManager.LoadScene("SecondScene");
                    }
                }
                else if (pointer.currentLetter != "Enter")
                {
                    nickName = nickName + pointer.currentLetter;
                    inputField.GetComponent<TMP_InputField>().text = nickName;
                }
            }   
        }

        if (inputs.RightMainTriggerUp)
        {
            if(pressedKey != null)
            {
                pressedKey.MakeSmaller();
            }
            
        }
    }


    //the Vivox stuff
    private async void startVivoxVoice(string userDisplayName)
    {
        await InitializeVivoxAsync();
        await LoginToVivoxAsync(userDisplayName);
        await VivoxService.Instance.JoinGroupChannelAsync(relayStarter.relayRoomCode, ChatCapability.AudioOnly, null);
        //await VivoxService.Instance.JoinEchoChannelAsync(relayStarter.relayRoomCode, ChatCapability.AudioOnly, null);
            //string channelName, ChatCapability chatCapability, ChannelOptions channelOptions = null)
    }

    public async Task InitializeVivoxAsync()
    {
        //already ran these when Relay started
        //await UnityServices.InitializeAsync();
        //await AuthenticationService.Instance.SignInAnonymouslyAsync();
        await VivoxService.Instance.InitializeAsync();
    }

    public async Task LoginToVivoxAsync(string userDisplayName)
    {
        LoginOptions options = new LoginOptions();
        options.DisplayName = userDisplayName;
        options.EnableTTS = true;
        await VivoxService.Instance.LoginAsync(options);
    }

    /*
    public void OnApplicationQuit()
    {
        VivoxService.Instance.LeaveChannelAsync(Variables.joinCode);
        VivoxService.Instance.LogoutAsync();
    }
    */
    

    
}
