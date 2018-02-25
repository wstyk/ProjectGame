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
    //[HideInInspector]
#if UNITY_EDITOR
    [String("Spell preferences:")]
#endif
    public string OffType;
    public string OffElement;
    public string DeffType;
    public string DeffElement;
    public string ChosenOffType;
    public string ChosenOffElement;
    public string ChosenDeffType;
    public string ChosenDeffElement;
#if UNITY_EDITOR
    [String("Spell info:")]
#endif
    public string Off1;
    public string Off2;
    public string Off3;
    public string Deff1;
    [HideInInspector]
    public GameObject ChoosingOff;
    [HideInInspector]
    public GameObject ChoosingDeff;
#if UNITY_EDITOR
    [String("Match info containers:")]
#endif
    public string matchName;
    public string matchPass;
    public uint matchSize;
    public bool isHosting;
    public bool isConnected;
    public string TypeInput;
    public string ElementInput;
#if UNITY_EDITOR
    [String("Input fields references:")]
#endif
    [SerializeField]
    InputField Name;
    [SerializeField]
    InputField Size;
    [SerializeField]
    InputField Pass;
#if UNITY_EDITOR
    [String("Menu canvas references:")]
#endif
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
    [SerializeField]
    GameObject OptionsCanvas;
    [SerializeField]
    GameObject GeneralOptions;
    [SerializeField]
    GameObject DisplayOptions;
    [SerializeField]
    GameObject SoundOptions;
    [SerializeField]
    GameObject ControlsOptions;
    GameObject ActiveCanvas;
#if UNITY_EDITOR
    [String("Skill trees containers:")]
#endif
    [SerializeField]
    List<GameObject> OffSkillTrees;
    [SerializeField]
    List<GameObject> DeffSkillTrees;
    [SerializeField]
    GameObject OffTreeGameObject;
    [SerializeField]
    GameObject DeffTreeGameObject;
#if UNITY_EDITOR
    [String("Offensive range trees:")]
#endif
    [SerializeField]
    GameObject FOR;
    [SerializeField]
    GameObject EOR;
    [SerializeField]
    GameObject WOR;
    [SerializeField]
    GameObject SOR;
    [SerializeField]
#if UNITY_EDITOR
    [String("Offensive melee trees:")]
#endif
    GameObject FOM;
    [SerializeField]
    GameObject EOM;
    [SerializeField]
    GameObject WOM;
    [SerializeField]
    GameObject SOM;
#if UNITY_EDITOR
    [String("Deffensive range trees:")]
#endif
    [SerializeField]
    GameObject FDR;
    [SerializeField]
    GameObject WDR;
    [SerializeField]
    GameObject SDR;
    [SerializeField]
    GameObject EDR;
#if UNITY_EDITOR
    [String("Deffensive melee trees:")]
#endif
    GameObject AOT, ADT, TREE;
    NetworkLobbyManager LobbyManager;
    NetworkLobbyManagerScript LobbyScript;
#if UNITY_EDITOR
    [String("Lobby list spaces:")]
#endif
    [SerializeField]
    GameObject RoomList;
    [SerializeField]
    GameObject PlayerList;
#if UNITY_EDITOR
    [String("After connection (UI elements):")]
#endif
    [SerializeField]
    GameObject AfterConnect;
    [SerializeField]
    GameObject StateText;
#if UNITY_EDITOR
    [String("UI elements references:")]
#endif
    public Button OffButton1;
    public Button OffButton2;
    public Button OffButton3;
    public Button DeffButton1;
    [SerializeField]
    SpellList spells;
    [SerializeField]
    GameObject FailedToText;

    
    void Awake()
    {
        HostingCanvas.SetActive(false);
        MainCanvas.SetActive(true);
        SearchingCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        SpellsCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
        ActiveCanvas = MainCanvas;

        LobbyManager = FindObjectOfType<NetworkLobbyManager>();
        LobbyScript = LobbyManager.GetComponent<NetworkLobbyManagerScript>();
        RoomPlayerList();

        isHosting = false;
        isConnected = false;

        if (PlayerPrefs.HasKey("AOT")) 
        {
            AOT = OffSkillTrees[PlayerPrefs.GetInt("AOT")];
            UpdateOffTree();
        }
        else AOT = FOR;
        if (PlayerPrefs.HasKey("ADT")) 
        {
            ADT = OffSkillTrees[PlayerPrefs.GetInt("ADT")];
            UpdateDeffTree();
        }
        else ADT = EDR;

        if (PlayerPrefs.HasKey("OffType") && PlayerPrefs.HasKey("OffElement")) 
        {
            OffType = PlayerPrefs.GetString("OffType");
            ChosenOffType = PlayerPrefs.GetString("OffType");
            OffElement = PlayerPrefs.GetString("OffElement");
            ChosenOffElement = PlayerPrefs.GetString("OffElement");
        }
        else
        {
            OffType = "Range";
            ChosenOffType = "Range";
            OffElement = "Fire";
            ChosenOffElement = "Fire";
        }

        if (PlayerPrefs.HasKey("DeffType") && PlayerPrefs.HasKey("DeffElement")) 
        {
            DeffType = PlayerPrefs.GetString("DeffType");
            ChosenDeffType = PlayerPrefs.GetString("DeffType");
            DeffElement = PlayerPrefs.GetString("DeffElement");
            ChosenDeffElement = PlayerPrefs.GetString("DeffElement");
        }
        else 
        {
            DeffType = "Range";
            ChosenDeffType = "Range";
            DeffElement = "Earth";
            ChosenDeffElement = "Earth";
        }

        PlayerPrefs.Save();

        if (PlayerPrefs.HasKey("Off1")) Off1 = PlayerPrefs.GetString("Off1");
        else PlayerPrefs.SetString("Off1", "");
        if (PlayerPrefs.HasKey("Off2")) Off2 = PlayerPrefs.GetString("Off2");
        else PlayerPrefs.SetString("Off2", "");
        if (PlayerPrefs.HasKey("Off3")) Off3 = PlayerPrefs.GetString("Off3");
        else PlayerPrefs.SetString("Off3", "");
        if (PlayerPrefs.HasKey("Deff1")) Deff1 = PlayerPrefs.GetString("Deff1");
        else PlayerPrefs.SetString("Deff1", "");
    }

    void Update()
    {
        if(ActiveCanvas == MainCanvas)
        {

        }
        if(ActiveCanvas == GameCanvas)
        {

        }
        if(ActiveCanvas == SearchingCanvas)
        {

        }
        if(ActiveCanvas == SpellsCanvas)
        {
            if(TREE != null && TREE.activeInHierarchy == true)
            {
                if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                    if (ChoosingOff != null) ChoosingOff.transform.localScale = new Vector3(1, 1, 1);
                    if (ChoosingDeff != null) ChoosingDeff.transform.localScale = new Vector3(1, 1, 1);
                    TREE.SetActive(false);
                }
            }
        }
    }

    //Menu hostowania
    public void StartCreating()
    {
        if (Off1 != "" && Off2 != "" && Off3 != "" && Deff1 != "")
        {
            ActiveCanvas.SetActive(false);
            HostingCanvas.SetActive(true);
            ActiveCanvas = HostingCanvas;
        }
        else
        {
            GameObject FailedText = Instantiate(FailedToText, ActiveCanvas.transform);
            FailedText.transform.SetSiblingIndex(0);
            FailedText.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            FailedText.GetComponent<Text>().text = "Choose all spells \nfirst";
        }
    }

    //Menu szukania
    public void StartSearching()
    {
        if (Off1 != "" && Off2 != "" && Off3 != "" && Deff1 != "")
        {
            ActiveCanvas.SetActive(false);
            SearchingCanvas.SetActive(true);
            ActiveCanvas = SearchingCanvas;
        }
        else
        {
            GameObject FailedText = Instantiate(FailedToText, ActiveCanvas.transform);
            FailedText.transform.SetSiblingIndex(0);
            FailedText.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            FailedText.GetComponent<Text>().text = "Choose all spells \nfirst";
        }
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

    //Wejście do opcji
    public void OptionsPanel()
    {
        ActivateRestCanvas(false);
        ActiveCanvas.SetActive(false);
        ActiveCanvas = OptionsCanvas;
        OptionsCanvas.SetActive(true);
        OptionsWindow(GeneralOptions);
    }

    //funkcja do konkretnych okienek w opcjach
    public void OptionsWindow(GameObject choosen)
    {
        GeneralOptions.SetActive(false);
        SoundOptions.SetActive(false);
        DisplayOptions.SetActive(false);
        ControlsOptions.SetActive(false);
        choosen.SetActive(true);
    }

    //Menu wyboru spelli
    public void ChooseSpells()
    {
        ActiveCanvas.SetActive(false);
        SpellsCanvas.SetActive(true);
        ActiveCanvas = SpellsCanvas;
        if(AOT != null) AOT.SetActive(true);
        if(ADT != null) ADT.SetActive(true);
        if (Off1 != "")
        {
            for (int i = 0; i < spells.Names.Count; i++)
            {
                if (Off1 == spells.Names[i]) 
                {
                    OffButton1.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    OffButton1.GetComponentInChildren<Image>().sprite = spells.Images[i];
                }
            }
        }
        else OffButton1.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
        if (Off2 != "")
        {
            for (int i = 0; i < spells.Names.Count; i++)
            {
                if (Off2 == spells.Names[i]) 
                {
                    OffButton2.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    OffButton2.GetComponentInChildren<Image>().sprite = spells.Images[i];
                }
            }
        }
        else OffButton2.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
        if (Off3 != "")
        {
            for (int i = 0; i < spells.Names.Count; i++)
            {
                if (Off3 == spells.Names[i])
                {
                    OffButton3.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    OffButton3.GetComponentInChildren<Image>().sprite = spells.Images[i];
                }
            }
        }
        else OffButton3.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
        if (Deff1 != "")
        {
            for (int i = 0; i < spells.Names.Count; i++)
            {
                if (Deff1 == spells.Names[i]) 
                {
                    DeffButton1.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    DeffButton1.GetComponentInChildren<Image>().sprite = spells.Images[i];
                }
            }
        }
        else DeffButton1.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
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
            else if (OffElement == "Earth") EarthOffRange();
            else if (OffElement == "Water") WaterOffRange();
            else if (OffElement == "Shadow") ShadowOffRange();
        }
        if(OffType == "Melee")
        {
            if(AOT != null) AOT.SetActive(false);
        }
    }
    public void UpdateDeffTree()
    {
        if(DeffType == "Range")
        {
            if (DeffElement == "Fire") FireDeffRange();
            else if (DeffElement == "Earth") EarthDeffRange();
            else if (DeffElement == "Water") WaterDeffRange();
            else if (DeffElement == "Shadow") ShadowDeffRange();
        }
        if(DeffType == "Melee")
        {
            if(ADT != null) ADT.SetActive(false);
        }
    }

    //Funkcje zapisu wybranego spella w wybranym slocie
    public void Offensive(Button spell)
    {
        if(ChoosingOff.name == "Off1")
        {
            if (OffType != ChosenOffType || OffElement != ChosenOffElement) ResetOff();
            ChosenOffType = OffType;
            ChosenOffElement = OffElement;
            OffButton1.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            OffButton1.GetComponentInChildren<Image>().sprite = spell.GetComponent<Image>().sprite;
            Off1 = spell.name;
            if(Off1 == Off2)
            {
                Off2 = "";
                OffButton2.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            }
            if (Off1 == Off3)
            {
                Off3 = "";
                OffButton3.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            }
            ChoosingOff.transform.localScale = new Vector3(1, 1, 1);
            TREE.SetActive(false);
        }
        else if (ChoosingOff.name == "Off2")
        {
            if (OffType != ChosenOffType || OffElement != ChosenOffElement) ResetOff();
            ChosenOffType = OffType;
            ChosenOffElement = OffElement;
            OffButton2.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            OffButton2.GetComponentInChildren<Image>().sprite = spell.GetComponent<Image>().sprite;
            Off2 = spell.name;
            if (Off2 == Off1)
            {
                Off1 = "";
                OffButton1.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            }
            if (Off2 == Off3)
            {
                Off3 = "";
                OffButton3.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            }
            ChoosingOff.transform.localScale = new Vector3(1, 1, 1);
            TREE.SetActive(false);
        }
        else if (ChoosingOff.name == "Off3")
        {
            if (OffType != ChosenOffType || OffElement != ChosenOffElement) ResetOff();
            ChosenOffType = OffType;
            ChosenOffElement = OffElement;
            OffButton3.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            OffButton3.GetComponentInChildren<Image>().sprite = spell.GetComponent<Image>().sprite;
            Off3 = spell.name;
            if (Off3 == Off1)
            {
                Off1 = "";
                OffButton1.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            }
            if (Off3 == Off2)
            {
                Off2 = "";
                OffButton2.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            }
            ChoosingOff.transform.localScale = new Vector3(1, 1, 1);
            TREE.SetActive(false);
        }
    }
    public void Deffensive(Button spell)
    {
        if(ChoosingDeff.name == "Deff1")
        {
            if (DeffType != ChosenDeffType || DeffElement != ChosenDeffElement) ResetDeff();
            ChosenDeffType = DeffType;
            ChosenDeffElement = DeffElement;
            DeffButton1.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            DeffButton1.GetComponentInChildren<Image>().sprite = spell.GetComponent<Image>().sprite;
            Deff1 = spell.name;
            ChoosingDeff.transform.localScale = new Vector3(1, 1, 1);
            TREE.SetActive(false);
        }
    }

    //Funkcje do resetowania slotów przy zmianie rodzaju lub żywiołu spelli które chce się posiadać
    void ResetOff()
    {
        OffButton1.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
        OffButton2.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
        OffButton3.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
        Off1 = "";
        Off2 = "";
        Off3 = "";
        PlayerPrefs.Save();
    }
    void ResetDeff()
    {
        DeffButton1.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
        Deff1 = "";
    }
    
    //Funkcja zapisania obecnego wyboru spelli
    public void SaveSpells()
    {
        PlayerPrefs.SetString("Off1", Off1);
        PlayerPrefs.SetString("Off2", Off2);
        PlayerPrefs.SetString("Off3", Off3);
        PlayerPrefs.SetString("Deff1", Deff1);
        PlayerPrefs.SetString("ChosenOffElement", ChosenOffElement);
        PlayerPrefs.SetString("ChosenDeffElement", ChosenDeffElement);
        PlayerPrefs.Save();
    }

    //Funckje zmiany drzewka
    void FireOffRange()
    {
        if(AOT != null) AOT.SetActive(false);
        FOR.SetActive(true);
        AOT = FOR;
        PlayerPrefs.SetInt("AOT", 0);
        PlayerPrefs.Save();
    }
    void EarthOffRange()
    {
        if (AOT != null) AOT.SetActive(false);
        EOR.SetActive(true);
        AOT = EOR;
        PlayerPrefs.SetInt("AOT", 1);
        PlayerPrefs.Save();
    }
    void WaterOffRange()
    {
        if (AOT != null) AOT.SetActive(false);
        WOR.SetActive(true);
        AOT = WOR;
        PlayerPrefs.SetInt("AOT", 2);
        PlayerPrefs.Save();
    }
    void ShadowOffRange()
    {
        if (AOT != null) AOT.SetActive(false);
        SOR.SetActive(true);
        AOT = SOR;
        PlayerPrefs.SetInt("AOT", 3);
        PlayerPrefs.Save();
    }

    void FireDeffRange()
    {
        if (ADT != null) ADT.SetActive(false);
        FDR.SetActive(true);
        ADT = FDR;
        PlayerPrefs.SetInt("ADT", 0);
        PlayerPrefs.Save();
    }
    void EarthDeffRange()
    {
        if (ADT != null) ADT.SetActive(false);
        EDR.SetActive(true);
        ADT = EDR;
        PlayerPrefs.SetInt("ADT", 1);
        PlayerPrefs.Save();
    }
    void WaterDeffRange()
    {
        if (ADT != null) ADT.SetActive(false);
        WDR.SetActive(true);
        ADT = WDR;
        PlayerPrefs.SetInt("ADT", 2);
        PlayerPrefs.Save();
    }
    void ShadowDeffRange()
    {
        if (ADT != null) ADT.SetActive(false);
        SDR.SetActive(true);
        ADT = SDR;
        PlayerPrefs.SetInt("ADT", 3);
        PlayerPrefs.Save();
    }

    //!!!KONIEC FUNKCJI ZMIENIAJĄCYCH DRZEWKO SKILLI!!!


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
