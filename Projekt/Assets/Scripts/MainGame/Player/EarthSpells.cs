using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EarthSpells : NetworkBehaviour {
    [String("Camera reference:")]
    [SerializeField]
    GameObject PlayerCam;
    float EarthWallCD;
    float EarthWallDistance;
    [String("Spells prefabs:")]
    [SerializeField]
    GameObject earthwallPrefab;
	//Wszystkie komentarze odnośnie działania skryptu znajdują się w skrypcie FireSpells;
	void Update () {
        CmdCooldowns();
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            gameObject.transform.eulerAngles = new Vector3(0, PlayerCam.transform.eulerAngles.y, 0);
            CmdEarthWall("earthWall");
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
        if(spell == "earthWall" && EarthWallCD <=0)
        {
            EarthWallCD = 3f;
            Instantiate(earthwallPrefab, gameObject.transform.position + gameObject.transform.forward * EarthWallDistance + new Vector3(0, -20, 0), gameObject.transform.rotation);
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
