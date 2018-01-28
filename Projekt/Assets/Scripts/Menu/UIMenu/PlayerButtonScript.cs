using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerButtonScript : NetworkBehaviour {
    [HideInInspector]
    public NetworkLobbyPlayer LobbyPlayer;
    void Update()
    {
        if (LobbyPlayer == null) Destroy(gameObject);
    }
}
