using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ThunderSpells : NetworkBehaviour {
#if UNITY_EDITOR
    [String("Camera reference:")]
#endif
    [SerializeField]
    GameObject PlayerCam;
    float ThunderCD;
    float ThunderDistance;
    float FlameThrowerDistance;
    [HideInInspector]
    public string Off1, Off2, Off3, Deff1;
#if UNITY_EDITOR
    [String("Spells prefab reference:")]
#endif
    [SerializeField]
    GameObject thunderPrefab;

    void Start()
    {
        ThunderCD = 3;
        ThunderDistance = 2f;
        if (PlayerPrefs.GetString("ChosenOffElement") == "Thunder")
        {
            Off1 = PlayerPrefs.GetString("Off1");
            Off2 = PlayerPrefs.GetString("Off2");
            Off3 = PlayerPrefs.GetString("Off3");
        }
        if (PlayerPrefs.GetString("ChosenDeffElement") == "Thunder")
        {
            Deff1 = PlayerPrefs.GetString("Deff1");
        }
    }
    //tworzenie spella takie jak Fire
    void Update () {
        CmdCooldowns();
        if (Input.GetKey(KeyCode.Mouse0))
        {
            CmdThunderSpell(Off1);
        }
    }
    [Command]
    void CmdThunderSpell(string spell)
    {
        RpcThunderSpell(spell);
    }
    [ClientRpc]
    void RpcThunderSpell(string spell)
    {
        if (spell == "Thunder" && ThunderCD <= 0)
        {
            ThunderCD = 1f;
            Instantiate(thunderPrefab, gameObject.transform.position + gameObject.transform.forward * ThunderDistance + new Vector3(0, 10, 0), gameObject.transform.rotation);
        }
        //tu nie patrzeć xD
    }

    [Command]
    void CmdCooldowns()
    {
        RpcCooldowns();
    }

    [ClientRpc]
    void RpcCooldowns()
    {
        if (ThunderCD > 0) ThunderCD -= Time.deltaTime;
    }
}
