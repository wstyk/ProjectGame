using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class NetworkLobbyManagerScript : NetworkLobbyManager {
    NetworkLobbyManager LobbyManager;
    NetworkLobbyPlayer LobbyPlayer;
    Menu menu;
    ButtonScript btnscript;
    [String("Lobby list spaces:")]
    public GameObject RoomList;
    public GameObject PlayerList;
    [String("Prefab references:")]
    [SerializeField]
    Button PlayerButton;
    [SerializeField]
    Button RoomButton;
    [SerializeField]
    GameObject StateText;
    [SerializeField]
    GameObject FailedToText;
    [String("Lists:")]
    [SerializeField]
    List<MatchInfoSnapshot> Matches;
    MatchInfoSnapshot CurrentMatch;
    List<bool> Players;
    [String("Match info fields:")]
    public string MatchPass;
    public string MatchName;
    bool ready;
    public bool host;
    public uint MatchSize;

    //!!!POCZĄTEK FUNKCJI ZWIĄZANYCH Z TWORZENIEM I ŁĄCZENIEM SIĘ Z GRĄ!!!
    void Awake()
    {
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        ready = true;
        host = false;
    }
    void Start()
    {
        StartMM();
    }

    //Zaczęcie matchmakera
    void StartMM()
    {
        MatchName = "default";
        MatchSize = 6;
        MatchPass = "";
        LobbyManager = gameObject.GetComponent<NetworkLobbyManager>();
        LobbyManager.StartMatchMaker();
    }

    //Tworzenie pokoju gry
    public void CreateGame()
    {
        Debug.Log("Creating game");
        LobbyManager.matchMaker.CreateMatch(MatchName, MatchSize, true, MatchPass, "", "", 0, 0, OnMatchCreate);
        menu.GameLobby();
        host = true;
        menu.isHosting = true;
    }
    public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if(success)
        {
            menu.ActivateRestCanvas(true);
        }
        else
        {
            
        }
        base.OnMatchCreate(success, extendedInfo, matchInfo);
    }

    //Szukanie pokojów gry
    public void ListRooms()
    {
        
        //Opróżnianie RoomList
        foreach (Transform transform in RoomList.transform)
        {
            Destroy(transform.gameObject);
        }
        LobbyManager.matchMaker.ListMatches(0, 20, "", false, 0, 0, OnMatchList);
        GameObject text = Instantiate(StateText);
        text.transform.SetParent(RoomList.transform);
        text.transform.position = new Vector3(0, 0, 0);
        text.GetComponent<Text>().text = "Loading games...";
    }

    //Odśweżanie listy
    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {   
        //Opróżnianie RoomList
        foreach (Transform transform in RoomList.transform)
        {
            Destroy(transform.gameObject);
        }
        if (success && matches.Count > 0)
        {
            //Dodawanie gier do RoomList
            Matches = matches;
            foreach (MatchInfoSnapshot CurrentGame in Matches)
            {
                Button Btn = Instantiate(RoomButton);
                btnscript = Btn.GetComponent<ButtonScript>();
                btnscript.match = CurrentGame;
                Btn.transform.SetParent(RoomList.transform);
                Btn.transform.position = new Vector3(0, 0, 0);
            }
        }
        else
        {
            GameObject text = Instantiate(StateText);
            text.transform.SetParent(RoomList.transform);
            text.transform.position = new Vector3(0.5f, 0, 0);
            text.GetComponent<Text>().text = "Couldn't find any games";
        }
        
    }

    //Wywoływana przez serwer po wykonaniu JoinMatch()
    public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        GameObject passPopUp = GameObject.FindGameObjectWithTag("PassPopUp");
        if(success)
        {
            menu.GameLobby();
            menu.ActivateRestCanvas(true);
            menu.isConnected = true;
            if (passPopUp != null) Destroy(passPopUp);
        }
        else
        {
            GameObject failed = Instantiate(FailedToText, GameObject.FindGameObjectWithTag("SearchingCanvas").transform);
            if(passPopUp != null)
            {
                failed.transform.localPosition = new Vector3(0, 50, 0);
                failed.GetComponentInChildren<Text>().text = "Failed to join!";
            }
            else
            {
                failed.transform.localPosition = new Vector3(0, 200, 0);
                failed.GetComponentInChildren<Text>().text = "Failed to join!";
            }
            
        }
        base.OnMatchJoined(success, extendedInfo, matchInfo);
        
    }

    //!!!KONIEC FUNKCJI ZWIĄZANYCH Z TWORZENIEM I ŁĄCZENIEM SIĘ Z GRĄ!!!

    //!!!POCZĄTEK FUNKCJI NetworkLobbyManager'a I ZWIĄZANYCH Z POŁĄCZONYMI GRACZAMI!!!


    //Funkcja wywoływana przez lokalnego gracza do indetyfikacji
    public void Player(NetworkLobbyPlayer player)
    {
        LobbyPlayer = player;
    }

    //Zmiana stanu gotowośći gracza
    public void Ready()
    {
        if(ready == true)
        {
            LobbyPlayer.SendReadyToBeginMessage();
            LobbyPlayer.gameObject.GetComponent<PlayerLobby>().CmdChangeState(ready);
            ready = false;
            
        }
        else
        {
            LobbyPlayer.SendNotReadyToBeginMessage();
            LobbyPlayer.gameObject.GetComponent<PlayerLobby>().CmdChangeState(ready);
            ready = true;
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }
}
