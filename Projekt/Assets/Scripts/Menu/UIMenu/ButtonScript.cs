using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

public class ButtonScript : NetworkBehaviour {

    NetworkLobbyManager LobbyManager;
    public MatchInfoSnapshot match;
    [SerializeField]
    GameObject PassPopUp;
    [SerializeField]
    Text text;
    Menu menu;
    void Start()
    {
        LobbyManager = FindObjectOfType<NetworkLobbyManager>();
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        text.text = "Name: " + match.name + " " + match.currentSize + "/" + match.maxSize;
        if (match.isPrivate) text.text += " : requires password";
        else text.text += " : public";
    }
    //Funkcja odpowiada za dołączanie do gry bądź stworzenie PopUp'u na wpisanie hasła
    public void Join()
    {
        if (match.isPrivate)
        {
            GameObject PassPop = Instantiate(PassPopUp, GameObject.FindGameObjectWithTag("SearchingCanvas").transform);
            PassPop.GetComponent<PassPopUpScript>().match = match;
        }
        else
        {
            LobbyManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, LobbyManager.OnMatchJoined);
            menu.isConnected = true;
        }
        
    }
}
