using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PassPopUpScript : NetworkBehaviour{

    [SerializeField]
    Image BlackBackground, Background;
    [SerializeField]
    InputField PassField;
    NetworkLobbyManager LobbyManager;
    public MatchInfoSnapshot match;
    public string Pass;
    Menu menu;

    void Start()
    {
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        BlackBackground.rectTransform.sizeDelta = new Vector3(Screen.width, Screen.height, 0);
        LobbyManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<NetworkLobbyManager>();
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Destroy(gameObject);
    }
    public void Join()
    {
        LobbyManager.matchMaker.JoinMatch(match.networkId, Pass, "", "", 0, 0, LobbyManager.OnMatchJoined);
        menu.isConnected = true;
    }
    public void Cancel()
    {
        Destroy(gameObject);
    }
    public void ChangePass()
    {
        Pass = PassField.text;
    }
}
