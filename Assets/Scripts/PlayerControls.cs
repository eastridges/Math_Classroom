using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

public class PlayerControls : NetworkBehaviour
{
    public InputReader inputs;
    public Transform Player;
    public TextMeshPro username;
    public Transform usernameTransform;

    //private string playerName;

    public NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>("username", NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        inputs = GameObject.FindGameObjectWithTag("input").GetComponent<InputReader>();
        Player = GameObject.Find("Main Camera").GetComponent<Transform>();
        usernameTransform = this.GetComponent<Transform>().GetChild(1);
        if (IsOwner)
        {
            SetPlayerNameServerRpc(GameController.nickName);
        }
        username = this.GetComponent<Transform>().GetChild(1).GetComponent<TextMeshPro>();
        username.SetText(playerName.Value.ToString());
        if (IsOwner)
        {
            this.transform.GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
        this.playerName.OnValueChanged += (oldVal, newVal) =>
        {
            username.SetText(playerName.Value.ToString());
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            Vector3 offset = new Vector3(0,1.36f,0);
            this.transform.position = Player.position - offset;
        }
        else
        {
            this.transform.GetChild(1).LookAt(new Vector3(2*this.transform.position.x - Player.position.x, this.transform.position.y, 2*this.transform.position.z - Player.position.z));
        }

    }

    public void Setup(InputReader loadInputs)
    {
        inputs=loadInputs;
    }

    [ServerRpc(RequireOwnership =false)]
    private void SetPlayerNameServerRpc(string name)
    {
        playerName.Value = name;
    }
    
}
