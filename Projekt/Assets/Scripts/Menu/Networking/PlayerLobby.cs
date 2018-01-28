using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class PlayerLobby : NetworkBehaviour {

    [SerializeField]
    Button PlayerButton;
    NetworkLobbyManager LobbyManger;
    NetworkLobbyPlayer LobbyPlayer;
    Button MyButton;
    GameObject PlayerList;
    Menu menu;
    //Skrypt identyfikujący lokalnego gracza
    void Start()
    {
        PlayerList = GameObject.FindGameObjectWithTag("PlayerList");
        LobbyManger = NetworkLobbyManager.singleton.GetComponent<NetworkLobbyManager>();
        LobbyPlayer = gameObject.GetComponent<NetworkLobbyPlayer>();
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        if(isLocalPlayer)
           LobbyManger.gameObject.GetComponent<NetworkLobbyManagerScript>().Player(gameObject.GetComponent<NetworkLobbyPlayer>());
        LocalEnable(LobbyPlayer.readyToBegin);
    }
    //Pierwsze uruchomienie
    void LocalEnable(bool state)
    {
        MyButton = Instantiate(PlayerButton);
        MyButton.GetComponent<PlayerButtonScript>().LobbyPlayer = gameObject.GetComponent<NetworkLobbyPlayer>();
        if (state) MyButton.GetComponent<Image>().color = new Color(0, 255, 0);
        else MyButton.GetComponent<Image>().color = new Color(255, 0, 0);
        MyButton.transform.SetParent(PlayerList.transform);
        
    }
    
    [Command]
    public void CmdChangeState(bool state)
    {
        RpcChangeState(state);
    }
    //Funkcja odpowiedzialna za zmianę wyglądu przycisku
    [ClientRpc]
    public void RpcChangeState(bool state)
    {
        if(state == true)
        {
            MyButton.GetComponent<Image>().color = new Color(0, 255, 0);
        }
        else
        {
            MyButton.GetComponent<Image>().color = new Color(255, 0, 0);
        }
    }
}
