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
    public string Off1, Off2, Off3, Deff1;
#if UNITY_EDITOR
    [String("Spells prefabs:")]
#endif
    [SerializeField]
    GameObject earthwallPrefab;
    void Start()
    {
        EarthWallDistance = 12f;
        if (PlayerPrefs.GetString("ChosenOffElement") == "Earth")
        {
            Off1 = PlayerPrefs.GetString("Off1");
            Off2 = PlayerPrefs.GetString("Off2");
            Off3 = PlayerPrefs.GetString("Off3");
        }
        if (PlayerPrefs.GetString("ChosenDeffElement") == "Earth")
        {
            Deff1 = PlayerPrefs.GetString("Deff1");
        }
    }
	//Wszystkie komentarze odnośnie działania skryptu znajdują się w skrypcie FireSpells;
	void Update () {
        CmdCooldowns();
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            CmdEarthWall(Deff1);
        }
	}

    [Command]
    void CmdEarthWall(string spell)
    {
        RpcEarthWall(spell);
    }

    //Spelle
    [ClientRpc]
    void RpcEarthWall(string spell)
    {
        //Ściana ziemii
        if(spell == "EarthWall" && EarthWallCD <=0)
        {
            EarthWallCD = 3f;
            Instantiate(earthwallPrefab, gameObject.transform.position + gameObject.transform.forward * EarthWallDistance + new Vector3(0, -10, 0), gameObject.transform.rotation);
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
