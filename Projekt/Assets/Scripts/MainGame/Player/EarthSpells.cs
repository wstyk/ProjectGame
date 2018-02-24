using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EarthSpells : NetworkBehaviour {
#if UNITY_EDITOR
    [String("Camera reference:")]
#endif
    [SerializeField]
    GameObject PlayerCam;
    float EarthWallCD;
    float EarthWallDistance;
    int Hold;
    public int EarthWallCount;
    [SerializeField]
    public List<GameObject> EarthWalls;
    public string Off1, Off2, Off3, Deff1, ChosenOff, ChosenDeff;
#if UNITY_EDITOR
    [String("Spells prefabs:")]
#endif
    [SerializeField]
    GameObject earthwallPrefab;
    void Start()
    {
        ChosenOff = PlayerPrefs.GetString("ChosenOffElement");
        ChosenDeff = PlayerPrefs.GetString("ChosenDeffElement");
        EarthWallDistance = 20f;
        EarthWallCount = 0;
        Hold = 0;
        if (ChosenOff == "Earth")
        {
            Off1 = PlayerPrefs.GetString("Off1");
            Off2 = PlayerPrefs.GetString("Off2");
            Off3 = PlayerPrefs.GetString("Off3");
        }
        if (ChosenDeff == "Earth")
        {
            Deff1 = PlayerPrefs.GetString("Deff1");
        }
        
    }
	//Wszystkie komentarze odnośnie działania skryptu znajdują się w skrypcie FireSpells;
	void Update () {
        CmdCooldowns();
        if (ChosenOff == "Earth")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) CmdSpell(Off1 + "Down");
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Hold = 0;
                CmdSpell(Off2 + "Up");
            }
            else if (Input.GetKey(KeyCode.Mouse0)) 
            {
                Hold++;
                if(Hold >= 10) CmdSpell(Off1 + "Hold");
            }

            if (Input.GetKeyDown(KeyCode.Mouse2)) CmdSpell(Off2 + "Down");
            else if (Input.GetKeyUp(KeyCode.Mouse2))
            {
                Hold = 0;
                CmdSpell(Off2 + "Up");
            }
            else if (Input.GetKey(KeyCode.Mouse2))
            {
                Hold++;
                if (Hold >= 10) CmdSpell(Off2 + "Hold");
            }

            if (Input.GetKeyDown(KeyCode.Mouse3)) CmdSpell(Off3 + "Down");
            else if (Input.GetKeyUp(KeyCode.Mouse3))
            {
                Hold = 0;
                CmdSpell(Off3 + "Up");
            }
            else if (Input.GetKey(KeyCode.Mouse3))
            {
                Hold++;
                if (Hold >= 10) CmdSpell(Off3 + "Hold");
            }
        }
        if (ChosenDeff == "Earth")
        {
            if (Input.GetKeyDown(KeyCode.Mouse1)) CmdSpell(Deff1 + "Down");
            else if (Input.GetKeyUp(KeyCode.Mouse1)) 
            {
                Hold = 0;
                CmdSpell(Deff1 + "Up");
            }
            else if (Input.GetKey(KeyCode.Mouse1))
            {
                Hold++;
                if (Hold >= 30) CmdSpell(Deff1 + "Hold");
            }
        }
    }

    [Command]
    void CmdSpell(string spell)
    {
        RpcSpell(spell);
    }

    //Spelle
    [ClientRpc]
    void RpcSpell(string spell)
    {
        //Ściana ziemii
        if(spell == "EarthWallDown" && EarthWallCD <=0)
        {
            EarthWallCD = 3f;
            EarthWallCount++;
            GameObject EarthWall = Instantiate(earthwallPrefab, gameObject.transform.position + gameObject.transform.forward * EarthWallDistance + new Vector3(0, -10, 0), gameObject.transform.rotation);
            EarthWall.GetComponent<EarthWallController>().scritp = gameObject.GetComponent<EarthSpells>();
            EarthWall.GetComponent<EarthWallController>().player = gameObject;
            EarthWalls.Add(EarthWall);
        }
        if(spell == "EarthWallHold" && EarthWallCount > 0)
        {
            Hold = 0;
            foreach(GameObject wall in EarthWalls)
            {
                wall.GetComponent<EarthWallController>().MoveWall();
            }
        }
    }

    [Command]
    void CmdCooldowns()
    {
        RpcCooldowns();
    }
    //Zmniejszanie cd
    [ClientRpc]
    void RpcCooldowns()
    {
        if (EarthWallCD > 0) EarthWallCD -= Time.deltaTime;
    }
}
