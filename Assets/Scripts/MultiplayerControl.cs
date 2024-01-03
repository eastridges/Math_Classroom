using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MultiplayerControl : NetworkBehaviour
{
    public GameObject Ball;

    [ServerRpc(RequireOwnership=false)]
    public void SpawnBallServerRpc(float x, float y, float z) {
        Vector3 spawnPosition = new Vector3(x,y,z);
        GameObject NewBall = Instantiate(Ball, spawnPosition, Quaternion.identity);
        NetworkObject NewBallNetwork = NewBall.GetComponent<NetworkObject>();
        NewBallNetwork.Spawn(true);
    }
}
