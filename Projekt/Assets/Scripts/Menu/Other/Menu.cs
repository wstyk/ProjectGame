using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    //Ten skrypt jest używany do poruszania się po menu,
    //jeżeli chcecie coś dodać do menu to tutaj powinny znajdować się do tego skrypty, 
    //jeżeli nie wymaga to tworzenia innego skryptu
    [String("Match info containers:")]
    public string matchName;
    public string matchPass;
    public uint matchSize;
    public bool isHosting;
    public bool isConnected;
    public string TypeInput;
    public string ElementInput;
    [String("Input fields references:")]
    [SerializeField]
    InputField Name;
    [SerializeField]
    InputField Size;
    [SerializeField]
    InputField Pass;
    [String("Menu canvas references:")]
    [SerializeField]
    GameObject MainCanvas;
    [SerializeField]
    GameObject HostingCanvas;
    [SerializeField]
    GameObject SearchingCanvas;
    [SerializeField]
    GameObject GameCanvas;
    [SerializeField]
    GameObject SpellsCanvas;
    GameObject ActiveCanvas;
    [String("Skill trees containers:")]
    [SerializeField]
    List<GameObject> OffSkillTrees;
    [SerializeField]
    List<GameObject> DeffSkillTrees;
    [SerializeField]
    GameObject OffTreeGameObject;
    [SerializeField]
    GameObject DeffTreeGameObject;
    [String("Tree GameObject references:")]
    [SerializeField]
    GameObject FOR;
    [SerializeField]
    GameObject TOR;
    [SerializeField]
    GameObject ED;
    GameObject AOT, ADT, TREE;
    NetworkLobbyManager LobbyManager;
    NetworkLobbyManagerScript LobbyScript;
    [String("Lobby list spaces:")]
    [SerializeField]
    GameObject RoomList;
    [SerializeField]
    GameObject PlayerList;
    [String("After connection (UI elements):")]
    [SerializeField]
    GameObject AfterConnect;
    [SerializeField]
    GameObject StateText;
    [String("UI elements references:")]
    public Button Off1;
    public Button Off2;
    public Button Off3;
    public Button Deff1;
    [HideInInspector]
    public string OffType, OffElement, DeffType, DeffElement;
    
    void Awake()
    {
        HostingCanvas.SetActive(false);
        MainCanvas.SetActive(true);
        SearchingCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        SpellsCanvas.SetActive(false);
        ActiveCanvas = MainCanvas;
        LobbyManager = FindObjectOfType<NetworkLobbyManager>();
        LobbyScript = LobbyManager.GetComponent<NetworkLobbyManagerScript>();
        RoomPlayerList();
        isHosting = false;
        isConnected = false;
        PlayerPrefs.SetInt("Spell", 1);
        if (PlayerPrefs.HasKey("AOT")) 
        {
            UpdateOffTree();
        }
        else AOT = FOR;
        if (PlayerPrefs.HasKey("ADT")) 
        {
            UpdateDeffTree();
        }
        else ADT = ED;

        if (PlayerPrefs.HasKey("OffType")) OffType = PlayerPrefs.GetString("OffType");
        else OffType = "Range";

        if (PlayerPrefs.HasKey("DeffType")) DeffType = PlayerPrefs.GetString("DeffType");
        else DeffType = "Range";

        if (PlayerPrefs.HasKey("OffElement")) OffElement = PlayerPrefs.GetString("OffElement");
        else OffElement = "Fire";

        if (PlayerPrefs.HasKey("DeffElement")) OffElement = PlayerPrefs.GetString("DeffElement");
        else DeffElement = "Earth";

        if (!PlayerPrefs.HasKey("Off1")) PlayerPrefs.SetString("Off1", "");
        if (!PlayerPrefs.HasKey("Off2")) PlayerPrefs.SetString("Off2", "");
        if (!PlayerPrefs.HasKey("Off3")) PlayerPrefs.SetString("Off3", "");
        if (!PlayerPrefs.HasKey("Deff1")) PlayerPrefs.SetString("Deff1", "");
    }

    //Menu hostowania
    public void StartCreating()
    {
        ActiveCanvas.SetActive(false);
        HostingCanvas.SetActive(true);
        ActiveCanvas = HostingCanvas;
    }

    //Menu szukania
    public void StartSearching()
    {
        ActiveCanvas.SetActive(false);
        SearchingCanvas.SetActive(true);
        ActiveCanvas = SearchingCanvas;
    }

    //Okienko gry
    public void GameLobby()
    {
        ActiveCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        ActiveCanvas = GameCanvas;
    }

    //Powrót do menu głównego
    public void Back()
    {
        ActivateRestCanvas(false);
        ActiveCanvas.SetActive(false);
        MainCanvas.SetActive(true);
        ActiveCanvas = MainCanvas;
    }

    //Menu wyboru spelli
    public void ChooseSpells()
    {
        ActiveCanvas.SetActive(false);
        SpellsCanvas.SetActive(true);
        ActiveCanvas = SpellsCanvas;
        if(AOT != null) AOT.SetActive(true);
        if(ADT != null) ADT.SetActive(true);
    }
    //Wyjście z gry
    public void Quit()
    {
        Application.Quit();
    }

    //Menu opcji
    public void Options()
    {

    }

    //!!!POCZĄTEK FUNKCJI ZMIENIAJĄCYCH DRZEWKO SKILLI!!!
    
    //Żeby nie pisać pełnych nazw drzewek będziemy tu się posługiwać skrótami.
    //Pierwsza litera oznacza żywioł, jeżeli będzie kiedyś potrzeba żeby użyć kilku znaków
    //zamiast 1 to kolejne powinny być pisane z małej, np. Fire byłoby Fi. Jako że narazie
    //wystarczą nam pojedyncze znaki to fire to F, earth to E itd.
    //Kolejna litera mówi czy ofensywne czy defensywne drzewko.
    //Ostania litra mówi czy zasięgowe czy melee. 
    //Przykład kodowania dla zasięgowego ofensywnego drzewka ognia to: F(ire)O(ffensive)R(ange), czyli FOR
    //AOT i ADT [ A(ctive)O(ffensive)T(ree) i A(ctive)D(evensive)T(ree) służą do wyłączania obecnie włączonego drzewka
    //Dodatkowym ułatwieniem powinien być fakt że nazwa funkcji w której użyty jest skrót jest jego rozwinięciem
    
    //Funkcja do wyświetlania okienka wyboru spelli
    public void ActivateOffTree()
    {
        if(TREE != null) TREE.SetActive(false);
        OffTreeGameObject.SetActive(true);
        TREE = OffTreeGameObject;
    }
    public void ActivateDeffTree()
    {
        if(TREE != null) TREE.SetActive(false);
        DeffTreeGameObject.SetActive(true);
        TREE = DeffTreeGameObject;
    }

    //Zmiana wyświetalnego drzewka
    public void UpdateOffTree()
    {
        if(OffType == "Range")
        {
            if (OffElement == "Fire") FireOffRange();
            if (OffElement == "Thunder") ThunderOffRange();
        }
        if(OffType == "Melee")
        {
            if(AOT != null) AOT.SetActive(false);
        }
    }
    public void UpdateDeffTree()
    {
        if(OffType == "Range")
        {
            if (OffElement == "Earth") EarthDeffRange();
        }
        if(OffType == "Melee")
        {
            if(AOT != null) AOT.SetActive(false);
        }
    }

    //Funkcje zapisu wybranego spella
    public void Offensive(Button spell)
    {
        if(PlayerPrefs.GetString("ChoosingOff") == "Off1")
        {
            Off1.GetComponent<Image>().sprite = spell.GetComponent<Image>().sprite;
            Debug.Log("OFF1");
            PlayerPrefs.SetString("Off1", spell.name);
        }
        if (PlayerPrefs.GetString("ChoosingOff") == "Off2")
        {
            Off2.GetComponent<Image>().sprite = spell.GetComponent<Image>().sprite;
            PlayerPrefs.SetString("Off2", spell.name);
        }
        if (PlayerPrefs.GetString("ChoosingOff") == "Off3")
        {
            Off3.GetComponent<Image>().sprite = spell.GetComponent<Image>().sprite;
            PlayerPrefs.SetString("Off3", spell.name);
        }
    }
    public void Deffensive(Button spell)
    {
        if(PlayerPrefs.GetString("ChoosingDeff") == "Deff1")
        {
            Deff1.GetComponent<Image>().sprite = spell.GetComponent<Image>().sprite;
            PlayerPrefs.SetString("Deff1", spell.name);
        }
    }
    
    //Funckje zmiany drzewka
    void FireOffRange()
    {
        if(AOT != null) AOT.SetActive(false);
        FOR.SetActive(true);
        AOT = FOR;
    }
    void ThunderOffRange()
    {
        if (AOT != null) AOT.SetActive(false);
        TOR.SetActive(true);
        AOT = TOR;
    }
    void EarthDeffRange()
    {
        if (ADT != null) ADT.SetActive(false);
        ED.SetActive(true);
        ADT = ED;
    }
    
    //!!!KONIEC FUNKCJI ZMIENIAJĄCYCH DRZEWKO SKULLI!!!


    //Odpowiednie dane dla LobbyManager'a
    void RoomPlayerList()
    {
        LobbyScript.RoomList = RoomList;
        LobbyScript.PlayerList = PlayerList;
    }

    public void Disconnect()
    {
        LobbyManager.StopHost();
        ActivateRestCanvas(false);
        HostingCanvas.SetActive(false);
        MainCanvas.SetActive(true);
        SearchingCanvas.SetActive(false);
        GameCanvas.SetActive(false);
    }

    public void ActivateRestCanvas(bool truefalse)
    {
        AfterConnect.SetActive(truefalse);
    }

    //Funkcje do uzupełniania parametrów gry (w menu hostowania)
    public void MatchSize()
    {
        matchSize = uint.Parse(Size.text);
        LobbyScript.MatchSize = matchSize;
    }
    public void MatchName()
    {
        matchName = Name.text;
        LobbyScript.MatchName = matchName;
    }
    public void MatchPass()
    {
        matchPass = Pass.text;
        LobbyScript.MatchPass = matchPass;
    }
}
