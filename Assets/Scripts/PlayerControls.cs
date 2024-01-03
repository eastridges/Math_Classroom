using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerControls : NetworkBehaviour
{
    public InputReader inputs;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GameObject.FindGameObjectWithTag("input").GetComponent<InputReader>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        Vector3 offset = new Vector3(0,0.0f,0);
        this.transform.position = Player.position - offset;
    }

    public void Setup(InputReader loadInputs)
    {
        inputs=loadInputs;
    }
}
