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
#if UNITY_EDITOR
    [String("Spells prefab reference:")]
#endif
    [SerializeField]
    GameObject thunderPrefab;

    void Start()
    {
        ThunderCD = 3;
        ThunderDistance = 2f;
    }
    //tworzenie spella takie jak Fire
    void Update () {
        CmdCooldowns();
        if (Input.GetKeyDown("q"))
        {
            CmdThunderSpell("thunder");
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
        if (spell == "thunder" && ThunderCD <= 0)
        {
            ThunderCD = 1f;
            Instantiate(thunderPrefab, gameObject.transform.position + gameObject.transform.forward * ThunderDistance + new Vector3(0, -2, 0), gameObject.transform.rotation);
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
