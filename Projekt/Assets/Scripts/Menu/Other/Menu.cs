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
    [String("Skill trees lists:")]
    [SerializeField]
    List<GameObject> OffSkillTrees;
    [SerializeField]
    List<GameObject> DeffSkillTrees;
    [String("Tree GameObject references:")]
    [SerializeField]
    GameObject FOR;
    [SerializeField]
    GameObject TOR;
    [SerializeField]
    GameObject ED;
    GameObject AOT, ADT;
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
    [String("DropDowns references:")]
    [SerializeField]
    Dropdown OffType;
    [SerializeField]
    Dropdown OffElement;
    [SerializeField]
    Dropdown DeffType;
    [SerializeField]
    Dropdown DeffElement;
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
            AOT = OffSkillTrees[PlayerPrefs.GetInt("AOT")];
            OffElement.value = PlayerPrefs.GetInt("AOT");
        }
        else AOT = FOR;
        if (PlayerPrefs.HasKey("ADT")) 
        {
            ADT = DeffSkillTrees[PlayerPrefs.GetInt("ADT")];
            DeffElement.value = PlayerPrefs.GetInt("ADT");
        }
        else ADT = ED;
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
        AOT.SetActive(true);
        ADT.SetActive(true);
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

    //2 funkcje do aktualizacji obecnych wartości które są w DropDownach
    public void UpdateOffTree()
    {
        TypeInput = OffType.options[OffType.value].text;
        ElementInput = OffElement.options[OffElement.value].text;
        ChangeOffTree(TypeInput, ElementInput);
    }
    public void UpdateDeffTree()
    {
        ElementInput = DeffElement.options[DeffElement.value].text;
        ChangeDeffTree(ElementInput);
    }

    //2 funkcje do wywowałania odpowiedniej funkcji zmiany drzewka
    void ChangeOffTree(string Type, string Element)
    {
        if (Type == "Range")
        {
            if (Element == "Fire") FireOffRange();
            if (Element == "Thunder") ThunderOffRange();
        }
        if(Type == "Melee")
        {

        }
    }
    public void ChangeDeffTree(string Element)
    {
        if (Element == "Earth") EarthDeff();
    }
    
    //Funckje zmiany drzewka
    void FireOffRange()
    {
        AOT.SetActive(false);
        FOR.SetActive(true);
        PlayerPrefs.SetInt("AOT", 0);
        AOT = FOR;
    }
    void ThunderOffRange()
    {
        AOT.SetActive(false);
        TOR.SetActive(true);
        PlayerPrefs.SetInt("AOT", 1);
        AOT = TOR;
    }
    void EarthDeff()
    {
        ADT.SetActive(false);
        ED.SetActive(true);
        PlayerPrefs.SetInt("ADT", 0);
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
